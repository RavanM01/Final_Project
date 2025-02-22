using AutoMapper;
using JobBoard.Business.DTOs.JobType;
using JobBoard.Business.DTOs.Skill;
using JobBoard.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoard.Business.Helpers.Mappers
{
    public class JobTypeMapper:Profile
    {
        public JobTypeMapper() {

            CreateMap<CreateJobTypeDto, JobType>().ReverseMap();
            CreateMap<GetJobTypeDto, JobType>().ReverseMap();

        }
    }
}
