using AutoMapper;
using JobBoard.Business.DTOs.Skill;
using JobBoard.Business.Services.Interfaces;
using JobBoard.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoard.Business.Helpers.Mappers
{
    public class SkillMapper:Profile
    {
        public SkillMapper() {

            CreateMap<CreateSkillDto,Skill>().ReverseMap();
            CreateMap<GetSkillDto,Skill>().ReverseMap();
        }
    }
}
