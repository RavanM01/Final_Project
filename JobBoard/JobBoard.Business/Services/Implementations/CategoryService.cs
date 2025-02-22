using AutoMapper;
using Azure;
using JobBoard.Business.DTOs.Category;
using JobBoard.Business.Exceptions.CommonExceptions;
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
    public class CategoryService : ICategoryService
    {
        readonly ICategoryRepo _repo;
        readonly IMapper _mapper;

        public CategoryService(IMapper mapper, ICategoryRepo repo)
        {
            _mapper = mapper;
            _repo = repo;
        }

        public async Task CreateAsync(CreateCategoryDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
            {
                throw new NullNameException($"Geçersiz giriş: {dto.Name}");
            }
            if(await _repo.IsExsist(x => x.Name.ToUpper() == dto.Name.ToUpper()))
            {
                throw new NullNameException($"Bu Category var: {dto.Name}");

            }
            var model = _mapper.Map<Category>(dto);
            var newmodel = await _repo.Create(model);
            await _repo.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var Category = await GetById(id);
            if (Category == null)
            {
                throw new Exception("yoxdu");
            }
            _repo.Delete(_mapper.Map<Category>(Category));
            await _repo.SaveChangesAsync();
        }

        public List<GetCategoryDto> GetAll()
        {
            var Categories = _repo.GetAll().ToList();
            return _mapper.Map<List<GetCategoryDto>>(Categories);
        }

        public async Task<GetCategoryDto> GetById(int id)
        {
            if (id <= 0)
            {
                throw new Exception();
            }
            GetCategoryDto dto = _mapper.Map<GetCategoryDto>(await _repo.GetbyId(id));

            return dto != null ? dto : throw new Exception();
        }

        public async Task SoftDelete(int id)
        {
            var Category = await GetById(id);
            _repo.SoftDelete(_mapper.Map<Category>(Category));
            await _repo.SaveChangesAsync();
        }

        public async Task Update(GetCategoryDto dto)
        {
            var oldCategory= await GetById(dto.Id);
            if (await _repo.IsExsist(x => x.Name.ToUpper() == dto.Name.ToUpper()))
            {
                throw new NullNameException($"Bu Category var: {dto.Name}");

            }
            if (string.IsNullOrWhiteSpace(dto.Name))
            {
                throw new NullNameException($"Geçersiz giriş: {dto.Name}");
            }
            oldCategory = _mapper.Map<GetCategoryDto>(dto);
            _repo.Update(_mapper.Map<Category>(oldCategory));
            await _repo.SaveChangesAsync();
        }
    }
}
