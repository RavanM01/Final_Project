using JobBoard.Business.DTOs.User;
using JobBoard.Business.Exceptions;
using JobBoard.Business.Helpers.Extensions;
using JobBoard.Business.Services.Implementations;
using JobBoard.Core.Entities;
using JobBoard.DAL.Context;
using JobBoard.Presentation.Areas.Manage.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobBoard.Presentation.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize]

    public class PanelController : Controller
    {
        UserManager<AppUser> userManager;
        AppDbContext dbContext;
        IWebHostEnvironment env;

        public PanelController(UserManager<AppUser> userManager, AppDbContext dbContext, IWebHostEnvironment env)
        {
            this.userManager = userManager;
            this.dbContext = dbContext;
            this.env = env;
        }

        public IActionResult Index()
        {
            var userId = userManager.GetUserId(User);
            var user = dbContext.Users.FirstOrDefault(x=>x.Id == userId);
            var favorites = dbContext.UserJobs
                .Include(x=>x.Job)
                    .ThenInclude(x=>x.JobType)
                .Include(x => x.Job)
                    .ThenInclude(x => x.Category)
                .Include(x => x.Job)
                    .ThenInclude(x => x.JobSkills)
                    .ThenInclude(x => x.Skill)
                .Include(x=>x.CreatedByUser)
                .Where(x=>x.UserId == userId&& x.Job.IsDeleted==false).ToList();
            var dto = new PanelDto();
            {
                dto.User = user;
                dto.userJobs= favorites;

            }
            return View(dto);
        }
        public IActionResult EditProfile(string? id)
        {
            var user = dbContext.Users.FirstOrDefault(x => x.Id == id);
            if (user == null)
            {
                throw new Exception();
            }
            EditProfileDto dto = new EditProfileDto();
            dto.Id =user.Id;
            dto.FullName = user.FullName;
            dto.Address = user.Address;
            dto.Description = user.Description;
           
            return View(dto);
        }
        [HttpPost]
        public async Task<IActionResult> EditProfile(EditProfileDto? dto)
        {
            if (dto == null)
            {
                return View(dto);
            }
            var olduser= dbContext.Users.FirstOrDefault(x => x.Id == dto.Id);
            if (olduser == null) {
                return NotFound();
            }
            if (dto.FullName == null)
            {
                ModelState.AddModelError("FullName", "Fullname daxil edin");
                return View(dto);

            }
            if (dto.Description == null)
            {
                ModelState.AddModelError("Description", "Description daxil edin");
                return View(dto);

            }
            if (dto.Address == null)
            {
                ModelState.AddModelError("Address", "Address daxil edin");
                return View(dto);

            }
            
            if (dto.LogoUrl == null)
            {
                ModelState.AddModelError("LogoUrl", "Sekil daxil edin");
                return View(dto);

            }
            if (!dto.LogoUrl.ContentType.Contains("image/"))
            {
                ModelState.AddModelError("LogoUrl", "Sekil daxil edin");
                return View(dto);
            }
            if (dto.LogoUrl.Length > 3000000)
            {
                ModelState.AddModelError("LogoUrl", "Max 2mb ola biler");
                return View();
            }
            if (dto.CvUrl == null)
            {
                ModelState.AddModelError("CvUrl", "Cv-ni daxil edin");
                return View(dto);

            }
            if (!(dto.CvUrl.ContentType == "application/pdf" ||
                dto.CvUrl.ContentType == "application/vnd.openxmlformats-officedocument.wordprocessingml.document" ||
                dto.CvUrl.ContentType == "application/msword"))
            {
                ModelState.AddModelError("CvUrl", "PDF,Doc ve ya Docx fayli yukleyin");
                return View();
            }
            if (dto.CvUrl.Length > 15728640)
            {
                ModelState.AddModelError("CvUrl", "Max 15mb ola biler");
                return View();
            }

            olduser.FullName = dto.FullName;
            olduser.CvUrl = dto.CvUrl.Upload(env.WebRootPath,"Upload/CVs");
            olduser.LogoUrl = dto.LogoUrl.Upload(env.WebRootPath, "Upload/UserPhoto");
            olduser.Address = dto.Address;
            olduser.Description = dto.Description;
            await dbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id <= 0)
            {
                throw new Exception("job cant be found");
            }
            try
            {
                var user = await userManager.GetUserAsync(User);
                var favjob = await dbContext.UserJobs.FirstOrDefaultAsync(x => x.Id == id && x.UserId == user.Id);
                if (favjob == null)
                {
                    throw new FavoriteNotFoundException();
                }
                dbContext.UserJobs.Remove(favjob);
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (FavoriteNotFoundException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(); 
            }

        }
    }
}
