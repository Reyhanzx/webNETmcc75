using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using webNETmcc75.Contexts;
using webNETmcc75.Models;
using webNETmcc75.Repositories;
using webNETmcc75.ViewModels;

namespace webNETmcc75.Controllers
{
    public class AccountController : Controller
    {
        private readonly AccountRepository repository;
        public AccountController(AccountRepository repository)
        {
            this.repository = repository;
        }
        public IActionResult Index()
        {
            var accounts = repository.GetAll;
            return View(accounts);
        }
        //public IActionResult Details(int id)
        //{
        //    var account = context.Accounts.Find(id);
        //    return View(account);
        //}
        //public IActionResult Create()
        //{
        //    return View();
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Create(Account account)
        //{
        //    context.Add(account);
        //    var result = context.SaveChanges();
        //    if (result > 0)
        //        return RedirectToAction(nameof(Index));
        //    return View();
        //}
        //public IActionResult Edit(int id)
        //{
        //    var account = context.Accounts.Find(id);
        //    return View(account);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Edit(Account account)
        //{
        //    context.Entry(account).State = EntityState.Modified;
        //    var result = context.SaveChanges();
        //    if (result > 0)
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View();
        //}
        //public IActionResult Delete(int id)
        //{
        //    var account = context.Accounts.Find(id);
        //    return View(account);
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Remove(int id)
        //{
        //    var account = context.Accounts.Find(id);
        //    context.Remove(account);
        //    var result = context.SaveChanges();
        //    if (result > 0)
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View();
        //}

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

                var result = repository.Register(registerVM);
                if (result == 0)
                {
                    return RedirectToAction("Index", "Home");
                }
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
            
           
            if (repository.Login(loginVM))
            {
                var userdata = repository.GetUserdata(loginVM.Email);
              
                

                HttpContext.Session.SetString("email", userdata.Email);
                HttpContext.Session.SetString("fullname", userdata.FullName);
                HttpContext.Session.SetString("role", userdata.Role);
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError(string.Empty, "salah");
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction(nameof(Index), "Home");
        }

        //public IActionResult ForgetPass()
        //{

        //    return View();

        //}

        //// POST : Account/ForgetPass
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult ForgetPass(LoginVM loginVM)
        //{


        //    if (repository.Login(loginVM))
        //    {
        //        return RedirectToAction("Index", "Home");
        //    }
        //    else
        //    {

        //    }
        //    ModelState.AddModelError(string.Empty, "salah");
        //    return View();
        //}
    }
}
