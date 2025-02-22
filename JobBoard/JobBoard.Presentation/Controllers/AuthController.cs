using AutoMapper;
using JobBoard.Business.DTOs.Mail;
using JobBoard.Business.DTOs.User;
using JobBoard.Business.Exceptions.UserExceptions;
using JobBoard.Business.Helpers.Extensions;
using JobBoard.Business.Services.Interfaces;
using JobBoard.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using System.Text;
using IMailService = JobBoard.Business.Services.Interfaces.IMailService;

namespace JobBoard.Presentation.Controllers
{
 
    public class AuthController : Controller
    {
        IAuthService authService;
        readonly IWebHostEnvironment _env;
        IMapper _mapper;
        UserManager<AppUser> _userManager;
        SignInManager<AppUser> _signInManager;
        IMailService mailService;
        public AuthController(IAuthService authService, IWebHostEnvironment env, IMapper mapper, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IMailService mailService)
        {
            this.authService = authService;
            _env = env;
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            this.mailService = mailService;
        }


        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }
            await authService.RegisterAsync(dto);
            return RedirectToAction("Login");
        }
        public IActionResult CompanyRegister()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CompanyRegister(CompanyRegisterDto dto, string stripeEmail, string stripeToken)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }    
            if (!dto.Logo.ContentType.Contains("image/"))
            {
                ModelState.AddModelError("Logo", "Sekil daxil edin");
                return View();
            }
            if (dto.Logo.Length > 3000000)
            {
                ModelState.AddModelError("Logo", "Max 2mb ola biler");
                return View();
            }
           dto.LogoUrl = dto.Logo.Upload(_env.WebRootPath, "Upload/UserPhoto");
            
            await authService.CompanyRegister(dto,stripeEmail, stripeToken);

            return RedirectToAction("Login");
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginDto dto)
        {
         

            if (!ModelState.IsValid)
            {
                return View(dto);
            }
            try
            {
                await authService.LoginAsync(dto);

            return RedirectToAction("index", "home");
            }
            catch (InvalidLoginException ex) 
            {
                ModelState.AddModelError("", ex.Message);
                return View(dto);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Bilinmeyen bir hata oluştu.");
                return View(dto);
            }
        }
        public async Task<IActionResult> CreateRole()
        {
            await authService.CreateRole();
            return RedirectToAction("index", "home");
        }
        public async Task<IActionResult> LogOut()
        {
            await authService.LogOut();
            return RedirectToAction("index", "home");
        }

        public async Task<IActionResult> ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordDto dto)
        {
            if (!ModelState.IsValid) { return View(); }
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null) { return NotFound(); }
            string token = await _userManager.GeneratePasswordResetTokenAsync(user);

            string link = Url.Action("ResetPassword", "Auth", new { userId = user.Id, token = token }, HttpContext.Request.Scheme);

            await mailService.SendEmailAsync(new MailRequest()
            {
                Subject = "Reset Password",
                ToEmail = dto.Email,
                Body = $"<p>{user.FullName} Resetliyin kodu </p><br><a href='{link}'>Reset Password</a>"
            });
            return RedirectToAction("Index", "home");
        }
        public async Task<IActionResult> ResetPassword(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) { return NotFound(); }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto dto, string userId, string token)
        {
            if (!ModelState.IsValid) { return View(); }
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) { return NotFound(); }
            var result = await _userManager.ResetPasswordAsync(user, token, dto.Password);
            return RedirectToAction("Login");
        }
    }
}
