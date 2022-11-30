using Auros.WebUI.Models.DataContexts;
using Auros.WebUI.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Auros.WebUI.Controllers
{
    public class HomeController : Controller
    {
        readonly AurosDbContext db;

        public HomeController(AurosDbContext db)
        {
            this.db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(Subscribe subscribe)
        {
            if (ModelState.IsValid)
            {
                if (!db.Subscribes.Any(c => c.Email == subscribe.Email.ToLower().Trim()))
                {
                    db.Subscribes.Add(subscribe);
                    db.SaveChanges();
                    ModelState.Clear();
                    ViewBag.Message = "Ugurla abune oldunuz";
                }
                else
                {
                    ModelState.Clear();
                    ViewBag.Message = "Artiq bu email ile qeydiyyat kecmisiniz";
                }
                return View();
            }

            return View(subscribe);
        }

        public IActionResult Store()
        {
            return View();
        }

        public IActionResult ProductDetail()
        {
            return View();
        }
    }
}