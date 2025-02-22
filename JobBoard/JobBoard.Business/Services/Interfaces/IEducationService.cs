using JobBoard.Business.DTOs.Education;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoard.Business.Services.Interfaces
{
    public interface IEducationService
    {
        Task CreateAsync(CreateEducationDto dto);
        Task<GetEducationDto> GetById(int id);
        List<GetEducationDto> GetAll();
        Task Update(GetEducationDto dto);
        Task Delete(int id);
        Task SoftDelete(int id);
    }
}
