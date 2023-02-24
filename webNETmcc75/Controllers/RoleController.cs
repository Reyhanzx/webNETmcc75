using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webNETmcc75.Contexts;
using webNETmcc75.Models;
using webNETmcc75.Repositories;

namespace webNETmcc75.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleRepository repository;
        public RoleController(RoleRepository repository)
        {
            this.repository = repository;
        }
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("email") == null)
            {
                return RedirectToAction("Unauthorized", "Error");
            }
            if (HttpContext.Session.GetString("role") != "Admin")
            {
                return RedirectToAction("Forbidden", "Error");
            }
            var role = repository.GetAll();
            return View(role);
        }
        public IActionResult Details(int id)
        {
            if (HttpContext.Session.GetString("email") == null)
            {
                return RedirectToAction("Unauthorized", "Error");
            }
            if (HttpContext.Session.GetString("role") != "Admin")
            {
                return RedirectToAction("Forbidden", "Error");
            }
            var role = repository.GetById(id);
            return View(role);
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
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Role role)
        {
            if (HttpContext.Session.GetString("email") == null)
            {
                return RedirectToAction("Unauthorized", "Error");
            }
            if (HttpContext.Session.GetString("role") != "Admin")
            {
                return RedirectToAction("Forbidden", "Error");
            }

            var result = repository.Insert(role);
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
            var role = repository.GetById(id);
            return View(role);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Role role)
        {
            if (HttpContext.Session.GetString("email") == null)
            {
                return RedirectToAction("Unauthorized", "Error");
            }
            if (HttpContext.Session.GetString("role") != "Admin")
            {
                return RedirectToAction("Forbidden", "Error");
            }

            var result = repository.Update(role);
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
            var role = repository.GetById(id);
            return View(role);
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
