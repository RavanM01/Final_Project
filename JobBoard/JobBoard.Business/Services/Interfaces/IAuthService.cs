using JobBoard.Business.DTOs.Category;
using JobBoard.Business.DTOs.User;
using JobBoard.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoard.Business.Services.Interfaces
{
    public interface IAuthService
    {
        public Task RegisterAsync(RegisterDto dto);
        public Task CompanyRegister(CompanyRegisterDto dto, string stripeEmail, string stripeToken);
        public Task LoginAsync(LoginDto dto);
        public Task CreateRole();
        public Task LogOut();



    }
}
