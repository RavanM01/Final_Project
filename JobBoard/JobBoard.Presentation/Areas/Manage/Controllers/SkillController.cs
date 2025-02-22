using JobBoard.Business.DTOs.Skill;
using JobBoard.Business.Helpers.Enums;
using JobBoard.Business.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobBoard.Presentation.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize(Roles = "Admin")]

    public class SkillController : Controller
    {
        ISkillService SkillService;

        public SkillController(ISkillService SkillService)
        {
            this.SkillService = SkillService;
        }
        public IActionResult Index()
        {
            var vm = SkillService.GetAll();
            //   var vm = _mapper.Map<List<GetPositionViewModel>>(positions);
            return View(vm);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateSkillDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(dto);
            }
            await SkillService.CreateAsync(dto);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update(int id)
        {
            var Skill = await SkillService.GetById(id);

            if (Skill == null)
            {
                return NotFound();
            }
            return View(Skill);
        }
        [HttpPost]
        public async Task<IActionResult> Update(GetSkillDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }
            await SkillService.Update(dto);
            return RedirectToAction("Index");

        }
        public async Task<IActionResult> Delete(int id)
        {
            await SkillService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
