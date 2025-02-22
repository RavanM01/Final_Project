using AutoMapper;
using JobBoard.Business.DTOs.User;
using JobBoard.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoard.Business.Helpers.Mappers
{
    public class UserMapper :Profile
    {
        public UserMapper() { 
        
            CreateMap<RegisterDto,AppUser>().ReverseMap();
            CreateMap<CompanyRegisterDto,AppUser>().ReverseMap();
            CreateMap<EditProfileDto, AppUser>().ReverseMap();



        }
    }
}
