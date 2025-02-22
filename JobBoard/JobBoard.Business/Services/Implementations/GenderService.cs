using AutoMapper;
using JobBoard.Business.DTOs.Gender;
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
    public class GenderService : IGenderService
    {
        readonly IGenderRepo _repo;
        readonly IMapper _mapper;

        public GenderService(IMapper mapper, IGenderRepo repo)
        {
            _mapper = mapper;
            _repo = repo;
        }

        public async Task CreateAsync(CreateGenderDto dto)
        {
            if (await _repo.IsExsist(x => x.Name == dto.Name))
            {
                throw new Exception("Olmaz");
            }
            var model = _mapper.Map<Gender>(dto);
            var newmodel = await _repo.Create(model);
            await _repo.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var Gender = await GetById(id);
            if (Gender == null)
            {
                throw new Exception("yoxdu");
            }
            _repo.Delete(_mapper.Map<Gender>(Gender));
            await _repo.SaveChangesAsync();
        }

        public List<GetGenderDto> GetAll()
        {
            var Genders = _repo.GetAll().ToList();
            return _mapper.Map<List<GetGenderDto>>(Genders);
        }

        public async Task<GetGenderDto> GetById(int id)
        {
            if (id <= 0)
            {
                throw new Exception();
            }
            GetGenderDto dto = _mapper.Map<GetGenderDto>(await _repo.GetbyId(id));

            return dto != null ? dto : throw new Exception();
        }

        public async Task SoftDelete(int id)
        {
            var Gender = await GetById(id);
            _repo.SoftDelete(_mapper.Map<Gender>(Gender));
            await _repo.SaveChangesAsync();
        }

        public async Task Update(GetGenderDto dto)
        {
            var oldGender = await GetById(dto.Id);
            if (await _repo.IsExsist(c => c.Name == dto.Name))
            {
                throw new Exception();
            }
            oldGender = _mapper.Map<GetGenderDto>(dto);
            _repo.Update(_mapper.Map<Gender>(oldGender));
            await _repo.SaveChangesAsync();
        }
    }
}
