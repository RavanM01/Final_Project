using AutoMapper;
using JobBoard.Business.DTOs.Category;
using JobBoard.Business.DTOs.Education;
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
    public class EducationService : IEducationService
    {
        readonly IEducationRepo _repo;
        readonly IMapper _mapper;

        public EducationService(IMapper mapper, IEducationRepo repo)
        {
            _mapper = mapper;
            _repo = repo;
        }

        public async Task CreateAsync(CreateEducationDto dto)
        {
            if (await _repo.IsExsist(x => x.Name == dto.Name))
            {
                throw new Exception("Olmaz");
            }
            var model = _mapper.Map<Education>(dto);
            var newmodel = await _repo.Create(model);
            await _repo.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var Education = await GetById(id);
            if (Education == null)
            {
                throw new Exception("yoxdu");
            }
            _repo.Delete(_mapper.Map<Education>(Education));
            await _repo.SaveChangesAsync();
        }

        public List<GetEducationDto> GetAll()
        {
            var Educations = _repo.GetAll().ToList();
            return _mapper.Map<List<GetEducationDto>>(Educations);
        }

        public async Task<GetEducationDto> GetById(int id)
        {
            if (id <= 0)
            {
                throw new Exception();
            }
            GetEducationDto dto = _mapper.Map<GetEducationDto>(await _repo.GetbyId(id));

            return dto != null ? dto : throw new Exception();
        }

        public async Task SoftDelete(int id)
        {
            var Education = await GetById(id);
            _repo.SoftDelete(_mapper.Map<Education>(Education));
            await _repo.SaveChangesAsync();
        }

        public async Task Update(GetEducationDto dto)
        {
            var oldEducation = await GetById(dto.Id);
            if (await _repo.IsExsist(c => c.Name == dto.Name))
            {
                throw new Exception();
            }
            oldEducation = _mapper.Map<GetEducationDto>(dto);
            _repo.Update(_mapper.Map<Education>(oldEducation));
            await _repo.SaveChangesAsync();
        }
    }
}
