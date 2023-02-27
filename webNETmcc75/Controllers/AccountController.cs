using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using webNETmcc75.Contexts;
using webNETmcc75.Models;
using webNETmcc75.Repositories;
using webNETmcc75.ViewModels;

namespace webNETmcc75.Controllers
{
    public class AccountController : Controller
    {
        private readonly AccountRepository repository;
        private readonly IConfiguration configuration;
        public AccountController(AccountRepository repository, IConfiguration configuration)
        {
            this.repository = repository;
            this.configuration = configuration;
        }
        public IActionResult Index()
        {
            var accounts = repository.GetAll() ;
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

                var roles = repository.GetRolesByNik(loginVM.Email);

                var claims = new List<Claim>()
                {
                new Claim(ClaimTypes.Email, userdata.Email),
                new Claim(ClaimTypes.Name, userdata.FullName)
                };

                foreach (var item in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, item));
                }

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]));
                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    issuer: configuration["JWT:Issuer"],
                    audience: configuration["JWT:Audience"],
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(10),
                    signingCredentials: signIn
                    );

                var generateToken = new JwtSecurityTokenHandler().WriteToken(token);

                HttpContext.Session.SetString("jwtoken", generateToken);

                //HttpContext.Session.SetString("email", userdata.Email);
                //HttpContext.Session.SetString("fullname", userdata.FullName);
                //HttpContext.Session.SetString("role", userdata.Role);
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
