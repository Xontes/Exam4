using Exam4.DAL;
using Exam4.Models;
using Exam4.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Exam4.Areas.Manage.Controllers
{ 
    [Area("Manage")]
    [Authorize(Roles = "Admin")]
    public class MChefController : Controller
    {
        AppDbContext _context;
        IWebHostEnvironment _environment;

        public MChefController(AppDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public IActionResult Index()
        {
            List<MChef> mChefs = _context.mChefs.ToList();
            return View(mChefs);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(MChef mChef) 
        {
            if(!ModelState.IsValid)
            {
                return View();
            }
            if (!mChef.formFile.ContentType.Contains("image"))
            {
                ModelState.AddModelError("", "THIS ISNT AN IMAGE STUPID");
            }
            if(mChef.formFile.Length > 20000000)
            {
                ModelState.AddModelError("", "Can't hold this much gng :(");
            }

            mChef.ImgUrl = FileCreateExtension.CreateFile(mChef.formFile, _environment.WebRootPath, "\\images\\");
            await _context.AddAsync(mChef);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int id)
        {
            var c = await _context.mChefs.FindAsync(id);
            _context.Remove(c);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Update(int id)
        {
            var c = _context.mChefs.FirstOrDefault(x => x.Id == id);
            return View(c);
        }
        [HttpPost]
        public async Task<IActionResult> Update(MChef mChef)
        {
            if(mChef.formFile != null)
            {

                if (!ModelState.IsValid)
                {
                    return View();
                }
                if (!mChef.formFile.ContentType.Contains("image"))
                {
                    ModelState.AddModelError("", "THIS ISNT AN IMAGE STUPID >:(");
                }
                if (mChef.formFile.Length > 20000000)
                {
                    ModelState.AddModelError("", "Can't hold this much gng :(");
                }

                mChef.ImgUrl = FileCreateExtension.CreateFile(mChef.formFile, _environment.WebRootPath, "\\images\\");

                _context.mChefs.Update(mChef);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            else
            {
                _context.mChefs.Update(mChef);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
        }
    }
}
