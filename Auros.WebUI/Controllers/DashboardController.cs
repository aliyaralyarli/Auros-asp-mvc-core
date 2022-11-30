using Auros.WebUI.Models.DataContexts;
using Auros.WebUI.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Auros.WebUI.Controllers
{
    public class DashboardController : Controller
    {
        readonly AurosDbContext db;
        public DashboardController(AurosDbContext db)
        {
            this.db = db;
        }


        public IActionResult UserDashboard()
        {
            var sesion = HttpContext.Session.Get("username");
            if (sesion != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        public IActionResult Cards()
        {
            var data = db.Cards.Where(c => c.DeleteTime == null).ToList();
            var sesion = HttpContext.Session.Get("username");
            if (sesion != null)
            {
                return View(data);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        public IActionResult Add()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Add(Card model)
        {
            if (ModelState.IsValid)
            {
                await db.Cards.AddAsync(model);
                await db.SaveChangesAsync();
                return RedirectToAction("Cards", "Dashboard");
            }
            return View(model);
        }

        public IActionResult Edit(int? value)
        {
            var item = db.Cards.FirstOrDefault(c => c.CardNumber == value);
            return View(item);
        }

        [HttpPost]
        public IActionResult Edit(Card card)
        {
            card.UpdateTime = DateTime.Now;
            db.Cards.Update(card);
            db.SaveChanges();
            return RedirectToAction(nameof(Cards));
        }

        public IActionResult Delete(int? value)
        {
            var item = db.Cards.FirstOrDefault(c => c.Id == value);
            return View(item);
        }
        

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteConfirm(int? id)
        {
            var card = db.Cards.FirstOrDefault(c => c.Id == id);
            card.DeleteTime = DateTime.Now;
            db.Cards.Update(card);
            db.SaveChanges();
            return RedirectToAction("Cards", "Dashboard");
        }

        public IActionResult AddFurniture()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddProduct()
        {
            foreach (var file in Request.Form.Files)
            {
                Product img = new Product();
                img.BrandName = file.FileName;

                MemoryStream ms = new MemoryStream();
                file.CopyTo(ms);
                img.ImageData = ms.ToArray();

                ms.Close();
                ms.Dispose();

                db.Products.Add(img);
                db.SaveChanges();
            }
            ViewBag.Message = "Image(s) stored in database!";
            return View("Furnitures");
        }

        [HttpPost]
        public ActionResult RetriveImage()
        {
            Product img = db.Products.OrderByDescending
            (i => i.Id).SingleOrDefault();
            string imageBase64Data =
            Convert.ToBase64String(img.ImageData);
            string imageDataURL =
            string.Format("data:image/jpg;base64,{0}",
            imageBase64Data);
            ViewBag.ImageTitle = img.BrandName;
            ViewBag.ImageDataUrl = imageDataURL;
            return View("Furnitures");
        }

        public ActionResult ShowAll()
        {
            var data = db.Products.ToList();
            return View(data);
        }

        public IActionResult Furnitures()
        {
            var sesion = HttpContext.Session.Get("username");
            if (sesion != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
    }
}