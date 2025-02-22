using JobBoard.Business.DTOs.Education;
using JobBoard.Business.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobBoard.Presentation.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize(Roles = "Admin")]


    public class EducationController : Controller
    {
        IEducationService EducationService;

        public EducationController(IEducationService EducationService)
        {
            this.EducationService = EducationService;
        }
        public IActionResult Index()
        {
            var vm = EducationService.GetAll();
            //   var vm = _mapper.Map<List<GetPositionViewModel>>(positions);
            return View(vm);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateEducationDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(dto);
            }
            await EducationService.CreateAsync(dto);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update(int id)
        {
            var Education = await EducationService.GetById(id);

            if (Education == null)
            {
                return NotFound();
            }
            return View(Education);
        }
        [HttpPost]
        public async Task<IActionResult> Update(GetEducationDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }
            await EducationService.Update(dto);
            return RedirectToAction("Index");

        }
        public async Task<IActionResult> Delete(int id)
        {
            await EducationService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
