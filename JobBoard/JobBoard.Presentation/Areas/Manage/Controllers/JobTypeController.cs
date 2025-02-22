using JobBoard.Business.DTOs.JobType;
using JobBoard.Business.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobBoard.Presentation.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize(Roles = "Admin")]


    public class JobTypeController : Controller
    {
        IJobTypeService JobTypeService;

        public JobTypeController(IJobTypeService JobTypeService)
        {
            this.JobTypeService = JobTypeService;
        }
        public IActionResult Index()
        {
            var vm = JobTypeService.GetAll();
            //   var vm = _mapper.Map<List<GetPositionViewModel>>(positions);
            return View(vm);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateJobTypeDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(dto);
            }
            await JobTypeService.CreateAsync(dto);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update(int id)
        {
            var JobType = await JobTypeService.GetById(id);

            if (JobType == null)
            {
                return NotFound();
            }
            return View(JobType);
        }
        [HttpPost]
        public async Task<IActionResult> Update(GetJobTypeDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }
            await JobTypeService.Update(dto);
            return RedirectToAction("Index");

        }
        public async Task<IActionResult> Delete(int id)
        {
            await JobTypeService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
