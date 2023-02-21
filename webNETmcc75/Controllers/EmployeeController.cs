using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using webNETmcc75.Contexts;
using webNETmcc75.Models;
using webNETmcc75.ViewModels;

namespace webNETmcc75.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly MyContext context;
        public EmployeeController(MyContext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {
            //var employees = context.Employees.ToList();
            var results = context.Employees.Select(e => new EmployeeVM
            {
                Nik = e.Nik,
                FirstName = e.FirstName,
                LastName = e.LastName,
                BirthDate = e.BirthDate,
                Gender = (ViewModels.GenderEnum)e.Gender,
                HireingDate = e.HireingDate,
                Email = e.Email,
                PhoneNumber = e.PhoneNumber
            }).ToList();
            return View(results);
        }
        public IActionResult Details(string id)
        {
            var employee = context.Employees.Find(id);
            return View(new EmployeeVM
            {
                Nik = employee.Nik,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                BirthDate = employee.BirthDate,
                Gender = (ViewModels.GenderEnum)employee.Gender,
                HireingDate = employee.HireingDate,
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber,
            });
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
            
            context.Add(new Employee
            {
                Nik= employee.Nik,
                FirstName= employee.FirstName,
                LastName= employee.LastName,
                BirthDate= employee.BirthDate,
                Gender= (Models.GenderEnum)employee.Gender,
                HireingDate= employee.HireingDate,
                Email= employee.Email,
                PhoneNumber= employee.PhoneNumber,
            } );
            var result = context.SaveChanges();
            if (result > 0)
                return RedirectToAction(nameof(Index));
            return View();
        }
        public IActionResult Edit(string id)
        {
            var employee = context.Employees.Find(id);
            return View(new EmployeeVM
            {
                Nik = employee.Nik,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                BirthDate = employee.BirthDate,
                Gender = (ViewModels.GenderEnum)employee.Gender,
                HireingDate = employee.HireingDate,
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber,
            });
            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EmployeeVM employee)
        {
            context.Entry(new Employee
            {
                Nik = employee.Nik,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                BirthDate = employee.BirthDate,
                Gender = (Models.GenderEnum)employee.Gender,
                HireingDate = employee.HireingDate,
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber,
            }).State = EntityState.Modified;
            var result = context.SaveChanges();
            if (result > 0)
            {
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        public IActionResult Delete(string id)
        {
            var employee = context.Employees.Find(id);
            return View(new EmployeeVM
            {
                Nik = employee.Nik,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                BirthDate = employee.BirthDate,
                Gender = (ViewModels.GenderEnum)employee.Gender,
                HireingDate = employee.HireingDate,
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber,
            });
            return View(employee);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Remove(string nik)
        {
            var employee = context.Employees.Find(nik);
            context.Remove(employee);
            var result = context.SaveChanges();
            if (result > 0)
            {
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
    }
}
