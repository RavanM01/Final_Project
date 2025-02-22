using JobBoard.Business.DTOs.Gender;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoard.Business.Services.Interfaces
{
    public interface IGenderService
    {
        Task CreateAsync(CreateGenderDto dto);
        Task<GetGenderDto> GetById(int id);
        List<GetGenderDto> GetAll();
        Task Update(GetGenderDto dto);
        Task Delete(int id);
        Task SoftDelete(int id);
    }
}
