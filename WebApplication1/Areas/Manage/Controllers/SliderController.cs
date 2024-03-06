using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.DataAccess;
using WebApplication1.Models;
using WebApplication1.ViewModels.SliderViewModels;

namespace WebApplication1.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class SliderController : Controller
    {

        private readonly ProniaDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public SliderController(ProniaDbContext dbContext, IWebHostEnvironment webHostEnvironment)
        {
            _context = dbContext;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Sliders.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateSliderVM sliderVM)
        {

            if (sliderVM.ImageFile==null)
            {
                return View();
            }

            if (!sliderVM.ImageFile.ContentType.StartsWith("image/")) 
            {
                ModelState.AddModelError("ImageFile", "Wrong file type");  
                return View();
            }

         
            if (sliderVM.ImageFile.Length > 2 * 1024 * 1024)
            {
                ModelState.AddModelError("ImageFile", "File max size is 2mb");
                return View();
            }

            if (!ModelState.IsValid)
            {
                return View(sliderVM);
            }



            using FileStream fileStream = new FileStream(Path.Combine
                (_webHostEnvironment.WebRootPath,"assets", "images","SliderImgs",sliderVM.ImageFile.FileName) 
                ,FileMode.Create);

            await sliderVM.ImageFile.CopyToAsync(fileStream);

            await _context.Sliders.AddAsync(new Slider
            {
                ImgUrl = sliderVM.ImageFile.FileName,
                Title = sliderVM.Title,
                Description = sliderVM.Description,
                ButtonText = sliderVM.ButtonText,
                Offer=sliderVM.Offer
            });


            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0) return BadRequest();
            var entity = await _context.Sliders.FindAsync(id);
            if (entity == null) return NotFound();
            _context.Sliders.Remove(entity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int id)
        {
            if (id < 1) return BadRequest();
            var entity = await _context.Sliders.FindAsync(id);
            if (entity == null) return NotFound();
            return View(entity);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, Slider slider)
        {
            if (slider.Title==null ||
                slider.Description==null ||
                slider.ButtonText==null ||
                slider.Offer==null ||
                slider.ImgUrl==null 
                )
            {
                ModelState.AddModelError("Input","bos ola bilmez");
                return View();
            }

            if (id < 1) return BadRequest();
            var entity = await _context.Sliders.FindAsync(id);
            if (entity == null) return NotFound();
            entity.Title = slider.Title;
            entity.Offer = slider.Offer;
            entity.Description = slider.Description;
            entity.ButtonText = slider.ButtonText;
            entity.ImgUrl = slider.ImgUrl;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
