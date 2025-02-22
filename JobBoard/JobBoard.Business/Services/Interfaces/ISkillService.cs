using JobBoard.Business.DTOs.Skill;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoard.Business.Services.Interfaces
{
    public interface ISkillService
    {
        Task CreateAsync(CreateSkillDto dto);
        Task<GetSkillDto> GetById(int id);
        List<GetSkillDto> GetAll();
        Task Update(GetSkillDto dto);
        Task Delete(int id);
        Task SoftDelete(int id);
    }
}
