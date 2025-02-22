using AutoMapper;
using AutoMapper.Internal;
using JobBoard.Business.DTOs.Category;
using JobBoard.Business.DTOs.User;
using JobBoard.Business.Exceptions.UserExceptions;
using JobBoard.Business.Helpers.Enums;
using JobBoard.Business.Helpers.Extensions;
using JobBoard.Business.Services.Interfaces;
using JobBoard.Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.WebPages.Html;
using IMailService = JobBoard.Business.Services.Interfaces.IMailService;

namespace JobBoard.Business.Services.Implementations
{
    public class AuthService : IAuthService
    {

        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        readonly IConfiguration _configuration;
        readonly IMapper _mapper;
        private readonly IMailService mailService;


        public AuthService(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, IConfiguration configuration, RoleManager<IdentityRole> roleManager, IMapper mapper, IMailService mailService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _configuration = configuration;
            _roleManager = roleManager;
            _mapper = mapper;
            this.mailService = mailService;
        }

        public async Task RegisterAsync(RegisterDto dto)
        {
            if (await _userManager.FindByEmailAsync(dto.Email) != null)
            {
                throw new InvalidRegisterException();
            }

            var appuser = _mapper.Map<AppUser>(dto);
            appuser.FullName = dto.Name + dto.SurName;
            appuser.UserName = dto.Email;
            var result = await _userManager.CreateAsync(appuser, dto.Password);
            if (!result.Succeeded)
            {
                StringBuilder sb = new StringBuilder();
                foreach (var err in result.Errors)
                {
                    sb.Append(err.Description + " ");
                }
                throw new Exception(sb.ToString());
            }
            await _userManager.AddToRoleAsync(appuser, "Member");

            await _signInManager.SignInAsync(appuser, false);
        }
        public async Task LoginAsync(LoginDto dto)
        {

            var user = await _userManager.FindByEmailAsync(dto.Email);

            if (user == null)
            {
                throw new InvalidLoginException();
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, true);

            if (result.IsLockedOut)
            {
                throw new Exception("User not found");
                
            }
            if (!result.Succeeded)
            {
                throw new InvalidLoginException("Giriş başarısız! Lütfen tekrar deneyin.");
            }
            await _signInManager.SignInAsync(user, true);

        }

        public async Task CreateRole()
        {
            foreach (var item in Enum.GetValues(typeof(UserRoles)))
            {
                await _roleManager.CreateAsync(new IdentityRole()
                {
                    Name = item.ToString(),
                });

            }
        }

        public async Task LogOut()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task CompanyRegister(CompanyRegisterDto dto, string stripeEmail, string stripeToken)
        {
            if (await _userManager.FindByEmailAsync(dto.Email) != null)
            {
                throw new Exception();
            }

            var appuser = _mapper.Map<AppUser>(dto);
            appuser.UserName = dto.Email;

            var optionCust = new CustomerCreateOptions
            {
                Email = stripeEmail,
                Name = appuser.FullName,
                Phone = "+994 50 66"
            };
            var serviceCust = new CustomerService();
            Customer customer = serviceCust.Create(optionCust);


            var optionsCharge = new ChargeCreateOptions
            {

                Amount = 1500,
                Currency = "USD",
                Description = "Publish Job amount",
                Source = stripeToken,
                ReceiptEmail = stripeEmail


            };
            var serviceCharge = new ChargeService();
            Charge charge = serviceCharge.Create(optionsCharge);
            if (charge.Status != "succeeded")
            {
                throw new Exception("odenis alinmadi");
            }


            var result = await _userManager.CreateAsync(appuser, dto.Password);
            if (!result.Succeeded)
            {
                StringBuilder sb = new StringBuilder();
                foreach (var err in result.Errors)
                {
                    sb.Append(err.Description + " ");
                }
                throw new Exception(sb.ToString());
            }
            await _userManager.AddToRoleAsync(appuser, "Company");

            await _signInManager.SignInAsync(appuser, false);
        }

 
    }
}
