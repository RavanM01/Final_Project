using AutoMapper;
using JobBoard.Business.DTOs.Category;
using JobBoard.Business.Exceptions.CommonExceptions;
using JobBoard.Business.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;

namespace JobBoard.Presentation.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize(Roles ="Admin")]

    public class CategoryController : Controller
    {
        ICategoryService categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }
        public IActionResult Index()
        {
            var vm = categoryService.GetAll();
         //   var vm = _mapper.Map<List<GetPositionViewModel>>(positions);
            return View(vm);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryDto dto)
        {
            if (string.IsNullOrEmpty(dto.Name))
            {
                ModelState.AddModelError("", "Kategori adı boş olamaz.");
                return View(dto);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(dto);
            }
            try
            {
                await categoryService.CreateAsync(dto);
            }
            catch (NullNameException ex) // Özel exception yakala
            {
                ModelState.AddModelError("", ex.Message); // Hata mesajını ekleyerek View'a gönder
            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update(int id)
        {
            var category = await categoryService.GetById(id);

            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }
        [HttpPost]
        public async Task<IActionResult> Update(GetCategoryDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }
            try
            {
                await categoryService.Update(dto);

            }
            catch (NullNameException ex) // Özel exception yakala
            {
                ModelState.AddModelError("", ex.Message); // Hata mesajını ekleyerek View'a gönder
            }
            return RedirectToAction("Index");
            
        }
        public async Task<IActionResult> Delete(int id)
        {
            await categoryService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
