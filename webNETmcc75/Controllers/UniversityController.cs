﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webNETmcc75.Contexts;
using webNETmcc75.Models;
using webNETmcc75.Repositories;

namespace webNETmcc75.Controllers
{   
    public class UniversityController : Controller
    {
        private readonly UniversityRepository repository;
        public UniversityController(UniversityRepository repository)
        {
            this.repository = repository;
        }
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("email") == null)
            {
                return RedirectToAction("Unauthorized", "Error");
            }
           
            var universities = repository.GetAll();
            return View(universities);
        }
        public IActionResult Details(int id)
        {
            if (HttpContext.Session.GetString("email") == null)
            {
                return RedirectToAction("Unauthorized", "Error");
            }
            
            var university = repository.GetById(id);
            return View(university);
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
        public IActionResult Create(University university)
        {
            if (HttpContext.Session.GetString("email") == null)
            {
                return RedirectToAction("Unauthorized", "Error");
            }
            if (HttpContext.Session.GetString("role") != "Admin")
            {
                return RedirectToAction("Forbidden", "Error");
            }
            var result = repository.Insert(university);
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
            var university = repository.GetById(id);
            return View(university);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(University university)
        {
            if (HttpContext.Session.GetString("email") == null)
            {
                return RedirectToAction("Unauthorized", "Error");
            }
            if (HttpContext.Session.GetString("role") != "Admin")
            {
                return RedirectToAction("Forbidden", "Error");
            }
            var result = repository.Update(university);
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
            var university = repository.GetById(id);
            return View(university);
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
