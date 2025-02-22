using JobBoard.Business.DTOs.Gender;
using JobBoard.Business.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobBoard.Presentation.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize(Roles = "Admin")]


    public class GenderController : Controller
    {
        IGenderService GenderService;

        public GenderController(IGenderService GenderService)
        {
            this.GenderService = GenderService;
        }
        public IActionResult Index()
        {
            var vm = GenderService.GetAll();
            //   var vm = _mapper.Map<List<GetPositionViewModel>>(positions);
            return View(vm);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateGenderDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(dto);
            }
            await GenderService.CreateAsync(dto);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update(int id)
        {
            var Gender = await GenderService.GetById(id);

            if (Gender == null)
            {
                return NotFound();
            }
            return View(Gender);
        }
        [HttpPost]
        public async Task<IActionResult> Update(GetGenderDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }
            await GenderService.Update(dto);
            return RedirectToAction("Index");

        }
        public async Task<IActionResult> Delete(int id)
        {
            await GenderService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
