using Auros.WebUI.Models.DataContexts;
using Auros.WebUI.Models.Entities.FormModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Text;

namespace Auros.WebUI.Controllers
{
    public class AccountController : Controller
    {
        readonly AurosDbContext db;

        public AccountController(AurosDbContext db)
        {
            this.db = db;
        }
        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Index(SignInFormModel user)
        {
            if (ModelState.IsValid)
            {
                if (!db.Users.Any(c => c.Username == user.Username.ToLower().Trim()))
                {
                    db.Users.Add(user);
                    user.CreatedDate = DateTime.Now;
                    db.SaveChanges();
                    ModelState.Clear();
                    ViewBag.Message = "Ugurla qeydiyyatdan kecdiniz";
                }
                else if (db.Users.Any(c => c.Username == user.Username.ToLower().Trim()))
                {
                    ModelState.Clear();
                    ViewBag.Message = "Bu ad ile istifadeci movcuddur";
                }
                else if (db.Users.Any(c => c.Email == user.Email.ToLower().Trim()))
                {
                    ModelState.Clear();
                    ViewBag.Message = "Artiq bu email ile qeydiyyat kecmisiniz";
                }
                return View();
            }
            else
            {
                ViewBag.Message = "Butun xanalari doldurun";
            }

            return View(user);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(SignInFormModel user)
        {
            if (ModelState.IsValid)
            {
                var obj = db.Users.Where(c => c.Username.Equals(user.Username) && c.Password.Equals(user.Password)).FirstOrDefault();
                
                if (obj != null)
                {
                    HttpContext.Session.Set("username", Encoding.Unicode.GetBytes("username"));

                    var userid = obj.Id.ToString();
                    var username = user.Username;
                    return RedirectToAction("UserDashboard", "Dashboard");
                }
                else
                {
                    ViewBag.Message = "Istifadeci adi ve ya sifre yanlisdir";
                }
                return View(user);
            }
            else
            {
                ViewBag.Message = "Butun xanalari doldurun";
            }
            return View();
        }

        public IActionResult Logout()
        {
            return View();
        }

        [HttpPost]
        public IActionResult LogoutConfirm()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Account");
        }
    }
}