using AutoMapper;
using JobBoard.Business.DTOs.Gender;
using JobBoard.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoard.Business.Helpers.Mappers
{
    public class GenderMapper : Profile
    {
        public GenderMapper()
        {
            CreateMap<CreateGenderDto,Gender>().ReverseMap();
            CreateMap<GetGenderDto,Gender>().ReverseMap();
        }
    }
}
