using AutoMapper;
using JobBoard.Business.DTOs.JobType;
using JobBoard.Business.Services.Interfaces;
using JobBoard.Core.Entities;
using JobBoard.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoard.Business.Services.Implementations
{
    internal class JobTypeService : IJobTypeService
    {
        readonly IJobTypeRepo _repo;
        readonly IMapper _mapper;

        public JobTypeService(IMapper mapper, IJobTypeRepo repo)
        {
            _mapper = mapper;
            _repo = repo;
        }

        public async Task CreateAsync(CreateJobTypeDto dto)
        {
            if (await _repo.IsExsist(x => x.Name == dto.Name))
            {
                throw new Exception("Olmaz");
            }
            var model = _mapper.Map<JobType>(dto);
            var newmodel = await _repo.Create(model);
            await _repo.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var JobType = await GetById(id);
            if (JobType == null)
            {
                throw new Exception("yoxdu");
            }
            _repo.Delete(_mapper.Map<JobType>(JobType));
            await _repo.SaveChangesAsync();
        }

        public List<GetJobTypeDto> GetAll()
        {
            var JobTypes = _repo.GetAll().ToList();
            return _mapper.Map<List<GetJobTypeDto>>(JobTypes);
        }

        public async Task<GetJobTypeDto> GetById(int id)
        {
            if (id <= 0)
            {
                throw new Exception();
            }
            GetJobTypeDto dto = _mapper.Map<GetJobTypeDto>(await _repo.GetbyId(id));

            return dto != null ? dto : throw new Exception();
        }

        public async Task SoftDelete(int id)
        {
            var JobType = await GetById(id);
            _repo.SoftDelete(_mapper.Map<JobType>(JobType));
            await _repo.SaveChangesAsync();
        }

        public async Task Update(GetJobTypeDto dto)
        {
            var oldJobType = await GetById(dto.Id);
            if (await _repo.IsExsist(c => c.Name == dto.Name))
            {
                throw new Exception();
            }
            oldJobType = _mapper.Map<GetJobTypeDto>(dto);
            _repo.Update(_mapper.Map<JobType>(oldJobType));
            await _repo.SaveChangesAsync();
        }
    }
}
