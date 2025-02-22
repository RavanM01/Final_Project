using AutoMapper;
using JobBoard.Business.DTOs.Education;
using JobBoard.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoard.Business.Helpers.Mappers
{
    public class EducationMapper:Profile
    {
        public EducationMapper() { 
        
            CreateMap<GetEducationDto,Education>().ReverseMap();
            CreateMap<CreateEducationDto,Education>().ReverseMap();
        }
    }
}
