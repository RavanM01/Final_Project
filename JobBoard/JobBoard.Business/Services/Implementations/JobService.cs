using AutoMapper;
using JobBoard.Business.DTOs.Category;
using JobBoard.Business.DTOs.Gender;
using JobBoard.Business.DTOs.Job;
using JobBoard.Business.DTOs.JobType;
using JobBoard.Business.Services.Interfaces;
using JobBoard.Core.Entities;
using JobBoard.DAL.Context;
using JobBoard.DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace JobBoard.Business.Services.Implementations
{
    public class JobService : IJobService
    {
        readonly AppDbContext _appDbContext;
        readonly IJobRepo _repo;
        readonly ISkillRepo _skillRepo;
        readonly IMapper _mapper;

        public JobService(IMapper mapper, IJobRepo repo, AppDbContext appDbContext, ISkillRepo skillRepo)
        {
            _mapper = mapper;
            _repo = repo;
            _appDbContext = appDbContext;
            _skillRepo = skillRepo;
        }
        public async Task CreateAsync(CreateJobDto dto, string userid, string stripeEmail, string stripeToken)
        {
            var model = _mapper.Map<Job>(dto);
            model.CreatedDate = DateTime.Now;
            var newmodel = await _repo.Create(model);
            await _repo.SaveChangesAsync();
            if (newmodel.Id <= 0)
            {
                throw new Exception("Job kaydedilemedi!"); // Debug için hata fırlat
            }

            var modelid = await _appDbContext.Jobs.FirstOrDefaultAsync(x => x.Id == newmodel.Id);
            if (dto.SkillIds != null)
            {
                foreach (var skillId in dto.SkillIds)
                {
                    if (!(await _skillRepo.IsExsist(x => x.Id == skillId)))
                    {
                        throw new Exception("bele skill yoxdu");
                    }
                    JobSkills jobSkill = new JobSkills();
                    {
                        jobSkill.SkillId = skillId;
                        jobSkill.JobId = modelid.Id;
                    }
                    _appDbContext.JobSkills.Add(jobSkill);

                }
                var user = _appDbContext.Users.FirstOrDefault(x => x.Id == userid);



                var optionCust = new CustomerCreateOptions
                {
                    Email = stripeEmail,
                    Name = user.FullName,
                    Phone = "+994 50 66"
                };
                var serviceCust = new CustomerService();
                Customer customer = serviceCust.Create(optionCust);


                var optionsCharge = new ChargeCreateOptions
                {

                    Amount = 500,
                    Currency = "USD",
                    Description = "Publish Job amount",
                    Source = stripeToken,
                    ReceiptEmail = stripeEmail


                };
                var serviceCharge = new ChargeService();
                Charge charge = serviceCharge.Create(optionsCharge);
                if (charge.Status != "succeeded")
                {
                    throw new Exception("odenis alinmadi");
                }
                var UserJob = new AppUserJob
                {
                    JobId = modelid.Id,
                    CreatedByUserId = user.Id,
                };
                await _appDbContext.UserJobs.AddAsync(UserJob);
                await _appDbContext.SaveChangesAsync();
            }
        }
        public async Task Delete(int id)
        {
            var job = await GetById(id);
            if (job == null)
            {
                throw new Exception("yoxdu");
            }
            _repo.Delete(_mapper.Map<Job>(job));
            await _repo.SaveChangesAsync();
        }

        public List<GetJobDto> GetAll()
        {
            var Jobs = _repo.GetAll().ToList();
            return _mapper.Map<List<GetJobDto>>(Jobs);
        }

        public async Task<GetJobDto> GetById(int id)
        {
            if (id <= 0)
            {
                throw new Exception();
            }
            GetJobDto dto = _mapper.Map<GetJobDto>(await _repo.GetbyId(id));

            return dto != null ? dto : throw new Exception();
        }

        public async Task SoftDelete(int id)
        {
            var job = await GetById(id);
            _repo.SoftDelete(_mapper.Map<Job>(job));
            await _repo.SaveChangesAsync();
        }

        public async Task Update(GetJobDto dto)
        {
            var oldJob = await GetById(dto.Id);

            oldJob = _mapper.Map<GetJobDto>(dto);
            _repo.Update(_mapper.Map<Job>(oldJob));
            await _repo.SaveChangesAsync();
        }
    }
}
