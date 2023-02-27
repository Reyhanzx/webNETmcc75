using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using webNETmcc75.Contexts;
using webNETmcc75.Models;
using webNETmcc75.Repositories;

namespace webNETmcc75.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RoleController : Controller
    {
        private readonly RoleRepository repository;
        public RoleController(RoleRepository repository)
        {
            this.repository = repository;
        }
        public IActionResult Index()
        {
           
            var role = repository.GetAll();
            return View(role);
        }
        public IActionResult Details(int id)
        {
           
            var role = repository.GetById(id);
            return View(role);
        }
        public IActionResult Create()
        {
            
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Role role)
        {
           

            var result = repository.Insert(role);
            if (result > 0)
                return RedirectToAction(nameof(Index));
            return View();
        }
        public IActionResult Edit(int id)
        {
           
            var role = repository.GetById(id);
            return View(role);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Role role)
        {

            var result = repository.Update(role);
            if (result > 0)
            {
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        public IActionResult Delete(int id)
        {
           
            var role = repository.GetById(id);
            return View(role);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Remove(int id)
        {
           
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
