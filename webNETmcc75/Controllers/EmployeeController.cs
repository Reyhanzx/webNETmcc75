using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Data;
using webNETmcc75.Contexts;
using webNETmcc75.Models;
using webNETmcc75.Repositories;
using webNETmcc75.ViewModels;

namespace webNETmcc75.Controllers
{
    [Authorize(Roles ="Admin")]
    public class EmployeeController : Controller
    {
        private readonly EmployeeRepository repository;
        public EmployeeController(EmployeeRepository repository)
        {
            this.repository = repository;
        }
        public IActionResult Index()
        {

            //var employees = context.Employees.ToList();
            var results = repository.GetAllEmployees();
            return View(results);
        }
        public IActionResult Details(string id)
        {
            
            var employee = repository.GetByIdEmployee(id);
            return View(employee);
        }
        
        public IActionResult Create()
        {
          
            //var employees = context.Employees.ToList()
            //    .Select(e => new SelectListItem
            //    {
            //        Value = e.Gender.ToString(),
            //        Text = e.Gender.ToString()
            //    });
            //ViewBag.Employee = employees;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(EmployeeVM employee)
        {
         
            var result = repository.Insert(new Employee
            {
                Nik = employee.Nik,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                BirthDate = employee.BirthDate,
                Gender = (Models.GenderEnum)employee.Gender,
                HireingDate = employee.HireingDate,
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber,
            });
            if (result > 0)
                return RedirectToAction(nameof(Index));
            return View();
        }
        public IActionResult Edit(string id)
        {
          
            var employee = repository.GetByIdEmployee(id);
            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EmployeeVM employee)
        {
          
            var result = repository.Update(new Employee
            {
                Nik = employee.Nik,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                BirthDate = employee.BirthDate,
                Gender = (Models.GenderEnum)employee.Gender,
                HireingDate = employee.HireingDate,
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber,
            });
            if (result > 0)
            {
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        public IActionResult Delete(string id)
        {
            
            var employee = repository.GetByIdEmployee(id);
            return View(employee);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Remove(string nik)
        {
            
            var result = repository.Delete(nik);
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
