using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TestPR.Models;

namespace TestPR.Controllers
{
    public class CategoryController : Controller
    {
        private ApplicationContext db;
        public CategoryController(ApplicationContext context)
        {
            db = context;
        }


        // GET: CategoryController
        public ActionResult Index()
        {
            return View(db.Category.ToArray());
        }


        // GET: CategoryController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CategoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category cat)
        {
            try
            {
                db.Category.Add(cat);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: CategoryController/Edit/5
        [HttpGet]
        public ActionResult Edit(int id)
        {
            Category cat = db.Category.Find(id);
            if (cat != null)
            {
                return View(cat);
            }
            return View();
        }

        // POST: CategoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Category cat)
        {
            try
            {
                db.Category.Update(cat);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

     
        // POST: CategoryController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                Category cat = db.Category.Find(id);
                if (cat == null)
                {
                    return NotFound();
                }
                db.Category.Remove(cat);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
