using JobBoard.Business.DTOs.Job;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoard.Business.Services.Interfaces
{
    public interface IJobService
    {
        Task CreateAsync(CreateJobDto dto, string userid, string stripeEmail, string stripeToken);
        Task<GetJobDto> GetById(int id);
        List<GetJobDto> GetAll();
        Task Update(GetJobDto dto);
        Task Delete(int id);
        Task SoftDelete(int id);
    }
}
