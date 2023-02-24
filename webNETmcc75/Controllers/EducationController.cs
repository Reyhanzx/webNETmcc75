using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using webNETmcc75.Contexts;
using webNETmcc75.Models;
using webNETmcc75.Repositories;
using webNETmcc75.ViewModels;

namespace webNETmcc75.Controllers
{
    public class EducationController : Controller
    {
        private readonly EducationRepository repository;
        private readonly UniversityRepository universityRepository;
        public EducationController(EducationRepository repository, UniversityRepository universityRepository)
        {
            this.repository = repository;
            this.universityRepository = universityRepository;
        }
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("email") == null)
            {
                return RedirectToAction("Unauthorized", "Error");
            }
            var result = repository.GetAllEducationUniversities();
            return View(result);
        }
        public IActionResult Details(int id)
        {
            if (HttpContext.Session.GetString("email") == null)
            {
                return RedirectToAction("Unauthorized", "Error");
            }
            var education = repository.GetByIdlEducationUniversities(id);
            return View(education); 
        }
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("email") == null)
            {
                return RedirectToAction("Unauthorized", "Error");
            }
            if (HttpContext.Session.GetString("role") != "Admin")
            {
                return RedirectToAction("Forbidden", "Error");
            }
            var universities = universityRepository.GetAll()
                .Select(u => new SelectListItem
                {
                    Value = u.Id.ToString(),
                    Text = u.Name
                });
            ViewBag.University = universities;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(EducationUniversityVM education)
        {
            if (HttpContext.Session.GetString("email") == null)
            {
                return RedirectToAction("Unauthorized", "Error");
            }
            if (HttpContext.Session.GetString("role") != "Admin")
            {
                return RedirectToAction("Forbidden", "Error");
            }
            var result = repository.Insert(new Education
            {
                Id = education.Id,
                Degree = education.Degree,
                GPA = education.GPA,
                Major = education.Major,
                UniversityId = Convert.ToInt16(education.UniversityName)
            });
            //if (result > 0)
            //    return RedirectToAction(nameof(Index));
            if (result > 0)
            
            return RedirectToAction(nameof(Index));
            
            return View();
        }
        public IActionResult Edit(int id)
        {
            if (HttpContext.Session.GetString("email") == null)
            {
                return RedirectToAction("Unauthorized", "Error");
            }
            if (HttpContext.Session.GetString("role") != "Admin")
            {
                return RedirectToAction("Forbidden", "Error");
            }
            var universities = universityRepository.GetAll()
                .Select(u => new SelectListItem
                {
                    Value = u.Id.ToString(),
                    Text = u.Name
                });
            ViewBag.University = universities;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EducationUniversityVM education)
        {
            if (HttpContext.Session.GetString("email") == null)
            {
                return RedirectToAction("Unauthorized", "Error");
            }
            if (HttpContext.Session.GetString("role") != "Admin")
            {
                return RedirectToAction("Forbidden", "Error");
            }
            var result = repository.Update(new Education
            {
                Id = education.Id,
                Degree = education.Degree,
                GPA = education.GPA,
                Major = education.Major,
                UniversityId = Convert.ToInt16(education.UniversityName)
            });
            if (result > 0)
            {
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        public IActionResult Delete(int id)
        {
            if (HttpContext.Session.GetString("email") == null)
            {
                return RedirectToAction("Unauthorized", "Error");
            }
            if (HttpContext.Session.GetString("role") != "Admin")
            {
                return RedirectToAction("Forbidden", "Error");
            }
            var education = repository.GetByIdlEducationUniversities(id);
            return View(education);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Remove(int id)
        {
            if (HttpContext.Session.GetString("email") == null)
            {
                return RedirectToAction("Unauthorized", "Error");
            }
            if (HttpContext.Session.GetString("role") != "Admin")
            {
                return RedirectToAction("Forbidden", "Error");
            }
            var result = repository.Delete(id);
            if (result == 0)
            {
                //data tidak ditemuakn
            }
            else
            {
                return RedirectToAction(nameof(Index));

            }
            return View();
        }
    }
}
