using AutoMapper;
using JobBoard.Business.DTOs.Job;
using JobBoard.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoard.Business.Helpers.Mappers
{
    public class JobMapper :Profile
    {
        public JobMapper() {
        
            CreateMap<CreateJobDto,Job>().ReverseMap();
            CreateMap<GetJobDto,Job>().ReverseMap();

        }
    }
}
