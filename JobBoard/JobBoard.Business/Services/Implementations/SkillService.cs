using AutoMapper;
using JobBoard.Business.DTOs.Skill;
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
    public class SkillService : ISkillService
    {
        readonly ISkillRepo _repo;
        readonly IMapper _mapper;

        public SkillService(IMapper mapper, ISkillRepo repo)
        {
            _mapper = mapper;
            _repo = repo;
        }

        public async Task CreateAsync(CreateSkillDto dto)
        {
            if (await _repo.IsExsist(x => x.Name == dto.Name))
            {
                throw new Exception("Olmaz");
            }
            var model = _mapper.Map<Skill>(dto);
            var newmodel = await _repo.Create(model);
            await _repo.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var Skill = await GetById(id);
            if (Skill == null)
            {
                throw new Exception("yoxdu");
            }
            _repo.Delete(_mapper.Map<Skill>(Skill));
            await _repo.SaveChangesAsync();
        }

        public List<GetSkillDto> GetAll()
        {
            var Skills = _repo.GetAll().ToList();
            return _mapper.Map<List<GetSkillDto>>(Skills);
        }

        public async Task<GetSkillDto> GetById(int id)
        {
            if (id <= 0)
            {
                throw new Exception();
            }
            GetSkillDto dto = _mapper.Map<GetSkillDto>(await _repo.GetbyId(id));

            return dto != null ? dto : throw new Exception();
        }

        public async Task SoftDelete(int id)
        {
            var Skill = await GetById(id);
            _repo.SoftDelete(_mapper.Map<Skill>(Skill));
            await _repo.SaveChangesAsync();
        }

        public async Task Update(GetSkillDto dto)
        {
            var oldSkill = await GetById(dto.Id);
            if (await _repo.IsExsist(c => c.Name == dto.Name))
            {
                throw new Exception();
            }
            oldSkill = _mapper.Map<GetSkillDto>(dto);
            _repo.Update(_mapper.Map<Skill>(oldSkill));
            await _repo.SaveChangesAsync();
        }
    }
}
