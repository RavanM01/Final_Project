using JobBoard.Business.DTOs.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoard.Business.Services.Interfaces
{
    public interface ICategoryService
    {
        Task CreateAsync(CreateCategoryDto dto);
        Task<GetCategoryDto> GetById(int id);
        List<GetCategoryDto> GetAll();
        Task Update(GetCategoryDto dto);
        Task Delete(int id);
        Task SoftDelete(int id);
    }
}
