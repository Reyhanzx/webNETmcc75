using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using webNETmcc75.Contexts;
using webNETmcc75.Models;
using webNETmcc75.ViewModels;

namespace webNETmcc75.Controllers
{
    public class AccountController : Controller
    {
        private readonly MyContext context;
        public AccountController(MyContext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {
            var accounts = context.Accounts.ToList();
            return View(accounts);
        }
        public IActionResult Details(int id)
        {
            var account = context.Accounts.Find(id);
            return View(account);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Account account)
        {
            context.Add(account);
            var result = context.SaveChanges();
            if (result > 0)
                return RedirectToAction(nameof(Index));
            return View();
        }
        public IActionResult Edit(int id)
        {
            var account = context.Accounts.Find(id);
            return View(account);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Account account)
        {
            context.Entry(account).State = EntityState.Modified;
            var result = context.SaveChanges();
            if (result > 0)
            {
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        public IActionResult Delete(int id)
        {
            var account = context.Accounts.Find(id);
            return View(account);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Remove(int id)
        {
            var account = context.Accounts.Find(id);
            context.Remove(account);
            var result = context.SaveChanges();
            if (result > 0)
            {
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        // GET : Account/Register
        public IActionResult Register()
        {
            var genders = new List<SelectListItem>{
            new SelectListItem
            {
                Value = "0",
                Text = "Male"
            },
            new SelectListItem
            {
                Value = "1",
                Text = "Female"
            },
        };

            ViewBag.Genders = genders;
            return View();
        }

        // POST : Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(RegisterVM registerVM)
        {
            if (ModelState.IsValid)
            {
                // Bikin kondisi untuk mengecek apakah data university sudah ada
              
                University university = new University
                {
                    Name = registerVM.UniversityName
                };
                if (context.Universities.Any(u => u.Name == university.Name) )
                {
                    university.Id = context.Universities.FirstOrDefault(u => u.Name == university.Name).Id;
                }
                else
                {
                    context.Universities.Add(university);
                    context.SaveChanges();
                }

                Education education = new Education
                {
                    Major = registerVM.Major,
                    Degree = registerVM.Degree,
                    GPA = registerVM.GPA,
                    UniversityId = university.Id
                };
                context.Educations.Add(education);
                context.SaveChanges();

                Employee employee = new Employee
                {
                    Nik = registerVM.Nik,
                    FirstName = registerVM.FirstName,
                    LastName = registerVM.LastName,
                    BirthDate = registerVM.BirthDate,
                    Gender = (Models.GenderEnum)registerVM.Gender,
                    HireingDate = registerVM.HireingDate,
                    Email = registerVM.Email,
                    PhoneNumber = registerVM.PhoneNumber,
                };
                context.Employees.Add(employee);
                context.SaveChanges();

                Account account = new Account
                {
                    EmployeeNik = registerVM.Nik,
                    Password = registerVM.Password
                };
                context.Accounts.Add(account);
                context.SaveChanges();

                AccountRole accountRole = new AccountRole
                {
                    AccountNik = registerVM.Nik,
                    RoleId = 2
                };

                context.AccountRoles.Add(accountRole);
                context.SaveChanges();

                Profiling profiling = new Profiling
                {
                    EmployeeId = registerVM.Nik,
                    EducationId = education.Id
                };
                context.Profilings.Add(profiling);
                context.SaveChanges();

                return RedirectToAction("Index", "Home");
            }
            return View();

        }

        // GET : Account/Login
        public IActionResult Login()
        {

            return View();
            
        }

        // POST : Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginVM loginVM)
        {
            var results = context.Employees.Join(
                context.Accounts,
                e => e.Nik,
                a => a.EmployeeNik,
           (e, a) => new LoginVM
           {
               Email = e.Email,
               Password = a.Password,
           });
           
            if (results.Any(e => e.Email == loginVM.Email && e.Password == loginVM.Password))
            {
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError(string.Empty, "salah");
            return View();
        }
    }
}
