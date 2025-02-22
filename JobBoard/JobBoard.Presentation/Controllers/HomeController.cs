using JobBoard.Core.Entities;
using JobBoard.DAL.Context;
using JobBoard.Presentation.Areas.Manage.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobBoard.Presentation.Controllers
{
    
    public class HomeController : Controller
    {
        UserManager<AppUser> userManager;
        AppDbContext db;

        public HomeController(UserManager<AppUser> userManager, AppDbContext db)
        {
            this.userManager = userManager;
            this.db = db;
        }

        public async Task<IActionResult> Index()
        {
            int pageSize = 10;
            int page = 1;  // Default dəyəri 1 olaraq təyin et

            // URL parametrindən page dəyərini alırıq
            if (Request.Query.ContainsKey("page"))
            {
                page = int.Parse(Request.Query["page"]);
            }

            int totalJobs = db.Jobs.Where(x => x.IsDeleted == false).Count();
            int totalPages = (int)Math.Ceiling(totalJobs / (double)pageSize);

            var jobs= db.Jobs
                .Include(x => x.Category)
                .Include(x => x.JobSkills)
                .ThenInclude(z => z.Skill)
                .Include(x => x.JobType)
                .Include(x => x.AppUserJobs)
                .ThenInclude(z => z.CreatedByUser)
                    .OrderByDescending(x => x.CreatedDate)
                .Where(x=>x.IsDeleted==false)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = page;
            ViewBag.Category = db.Categories?.ToList() ?? new List<Category>();
            ViewBag.JobType = db.JobTypes?.ToList() ?? new List<JobType>();
            var totalJob = await db.Jobs.CountAsync();

            var memberCount = await db.Users
            .Where(u => db.UserRoles.Any(ur => ur.UserId == u.Id && db.Roles.Any(r => r.Id == ur.RoleId && r.Name == "Member")))
            .CountAsync();
            var companyCount = await db.Users
            .Where(u => db.UserRoles.Any(ur => ur.UserId == u.Id && db.Roles.Any(r => r.Id == ur.RoleId && r.Name == "Company")))
            .CountAsync();

            var stats = new Stats()
            {
                TotalJobs = totalJob,
                TotalCandidates = memberCount,
                TotalCompanies = companyCount
            };

            var dto = new IndexDto()
            {
                Jobs = jobs,
                Stats = stats
            };


            return View(dto);
        }

        public IActionResult singleJob(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }
            var job = db.Jobs
                 .Include(x => x.Category)
                .Include(x => x.JobSkills)
                .ThenInclude(z => z.Skill)
                .Include(x => x.JobType)
                .Include(x => x.Gender)
                .Include(x => x.Education)
                .Include(x => x.AppUserJobs)
                .ThenInclude(z => z.CreatedByUser).FirstOrDefault(x => x.Id == id);
            if (job == null)
            {
                return BadRequest();
            }
            var logoUrl = job.AppUserJobs.FirstOrDefault()?.CreatedByUser.LogoUrl;
            Console.WriteLine(logoUrl);
            return View(job);
        }
        public IActionResult Contact()
        {
            return View();
        }
        public IActionResult jobList()
        {
            ViewBag.Category = db.Categories?.ToList() ?? new List<Category>();
            ViewBag.JobType = db.JobTypes?.ToList() ?? new List<JobType>();

            return View();
        }
        [HttpPost]
        public IActionResult joblist(string jobTitle, int? categoryId, int? jobTypeId)
        {
            int pageSize = 10;
            int page = 1;  // Default dəyəri 1 olaraq təyin et

            // URL parametrindən page dəyərini alırıq
            if (Request.Query.ContainsKey("page"))
            {
                page = int.Parse(Request.Query["page"]);
            }


            var jobs = db.Jobs.Include(x => x.Category)
                .Include(x => x.JobSkills)
                .ThenInclude(z => z.Skill)
                .Include(x => x.JobType)
                .Include(x => x.Gender)
                .Include(x => x.Education)
                .Include(x => x.AppUserJobs)
                .ThenInclude(z => z.CreatedByUser).AsQueryable();

            if (!string.IsNullOrEmpty(jobTitle))
            {
                jobs = jobs.Where(j => j.Title.Contains(jobTitle));
            }

            if (categoryId.HasValue && categoryId > 0)
            {
                jobs = jobs.Where(j => j.CategoryId == categoryId);
            }

            if (jobTypeId.HasValue && jobTypeId > 0)
            {
                jobs = jobs.Where(j => j.JobTypeId == jobTypeId);
            }

            var jobList = jobs.ToList();

            int totalJobs = jobs.Count();
            int totalPages = (int)Math.Ceiling(totalJobs / (double)pageSize);

            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = page;

            return View(jobList); 
        }

      
        [HttpPost]
        public async Task<IActionResult> AddToFavorites(int jobId)
        {
            var userId = userManager.GetUserId(User);
            if (userId == null)
            {
                return Json(new { success = false, message = "User not logged in." });
            }

            var existingFavorite = await db.UserJobs
                .FirstOrDefaultAsync(uj => uj.UserId == userId && uj.JobId == jobId);

            if (existingFavorite == null)
            {
                var Byjob = db.UserJobs.FirstOrDefault(x=>x.JobId == jobId);
                if (Byjob == null)
                {
                    return NotFound();
                }
                var userJob = new AppUserJob
                {
                    UserId = userId,
                    JobId = jobId,
                    CreatedByUserId= Byjob.CreatedByUserId
                };

                db.UserJobs.Add(userJob);
                await db.SaveChangesAsync();

                return Json(new { success = true, message = "Added to favorites!" });
            }

            return Json(new { success = false, message = "Already in favorites!" });
        }
    }
}
