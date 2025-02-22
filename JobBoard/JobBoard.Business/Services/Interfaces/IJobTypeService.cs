using JobBoard.Business.DTOs.JobType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoard.Business.Services.Interfaces
{
    public interface IJobTypeService
    {
        Task CreateAsync(CreateJobTypeDto dto);
        Task<GetJobTypeDto> GetById(int id);
        List<GetJobTypeDto> GetAll();
        Task Update(GetJobTypeDto dto);
        Task Delete(int id);
        Task SoftDelete(int id);
    }
}
