using JobBoard.Business.DTOs.Job;
using JobBoard.Business.DTOs.JobType;
using JobBoard.Business.Services.Interfaces;
using JobBoard.Core.Entities;
using JobBoard.DAL.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobBoard.Presentation.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize(Roles = "Admin,Company")]
    

    public class JobController : Controller
    {
        AppDbContext dbContext;
        IJobService jobService;
        UserManager<AppUser> userManager;

        public JobController(AppDbContext dbContext, IJobService jobService, UserManager<AppUser> userManager)
        {
            this.dbContext = dbContext;
            this.jobService = jobService;
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            var currentuser = userManager.GetUserId(User);

            var jobs = dbContext.Jobs
            .Include(x => x.Category)
            .Include(x => x.JobSkills)
             .ThenInclude(z => z.Skill)
             .Include(x => x.JobType)
            .Include(x => x.AppUserJobs)
            .Where(x => x.AppUserJobs.Any(uj => uj.CreatedByUserId == currentuser)|| x.IsDeleted==false) 
            .ToList();

            return View(jobs);
        }
        public IActionResult Create()
        {
            ViewBag.Categories = dbContext.Categories.ToList();
            ViewBag.Educations = dbContext.Educations.ToList();
            ViewBag.JobTypes = dbContext.JobTypes.ToList();
            ViewBag.Genders = dbContext.Genders.ToList();
            ViewBag.Skills = dbContext.Skills.ToList();

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateJobDto dto,string stripeEmail, string stripeToken)
        {
            ViewBag.Categories = dbContext.Categories.ToList();
            ViewBag.Educations = dbContext.Educations.ToList();
            ViewBag.JobTypes = dbContext.JobTypes.ToList();
            ViewBag.Genders = dbContext.Genders.ToList();
            ViewBag.Skills = dbContext.Skills.ToList();

            var currentuser = userManager.GetUserId(User);


            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                              .Select(e => e.ErrorMessage)
                                              .ToList();

                ViewBag.Errors = errors; // Alternatif olarak ViewData["Errors"] = errors;
                return View();
            }

            await jobService.CreateAsync(dto, currentuser,stripeEmail, stripeToken);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Update(int id)
        {
            ViewBag.Categories = dbContext.Categories.ToList();
            ViewBag.Educations = dbContext.Educations.ToList();
            ViewBag.JobTypes = dbContext.JobTypes.ToList();
            ViewBag.Genders = dbContext.Genders.ToList();
            ViewBag.Skills = dbContext.Skills.ToList();

            var Job = await jobService.GetById(id);

            if (Job == null)
            {
                return NotFound();
            }
            return View(Job);
        }
        [HttpPost]
        public async Task<IActionResult> Update(GetJobDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }
            await jobService.Update(dto);
            return RedirectToAction("Index");

        }

        public async Task<IActionResult> Delete(int id)
        {
            await jobService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
