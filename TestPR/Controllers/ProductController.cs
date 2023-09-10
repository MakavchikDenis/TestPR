using Humanizer.Localisation.TimeToClockNotation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;
using System.Globalization;
using System.Net;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using TestPR.Models;

namespace TestPR.Controllers
{
    public class ProductController : Controller
    {
        private ApplicationContext db;
        public ProductController(ApplicationContext context) {
            db = context;
        }

        // GET: ProductController
        [HttpGet]
        public ActionResult Index(int? category, string? name)
        {
            IQueryable<Product> pr = db.Product.Include(p => p.category);
            if (category != null && category != 0)
            {
                pr = pr.Where(p => p.categoryId == category);
            }
            if (!string.IsNullOrEmpty(name))
            {
                pr = pr.Where(p => p.Name!.Contains(name));
            }

            List<Category> categories = db.Category.ToList();
            // устанавливаем начальный элемент, который позволит выбрать всех
            categories.Insert(0, new Category { Name = "Все", Id = 0 });

            UserListViewModel viewModel = new UserListViewModel
            {
                Products = pr.ToList(),
                Categories = new SelectList(categories, "Id", "Name", category),
                Name = name
            };
            return View(viewModel);

            //var pr = db.Product.Include(u => u.category).ToList();
            //return View(pr.ToArray());
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Categories = new SelectList(db.Category.ToList(), "Id", "Name");
            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product pr)
        {
            try
            {
                db.Product.Add(pr);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");               
            }
            catch
            {
                return View();
            }
        }

       
        [HttpGet]
        public ActionResult Edit(int? id)
        {           
            
            Product pr = db.Product.Find(id);
            if (pr != null)
            {
                ViewBag.Categories = new SelectList(db.Category.ToList(), "Id", "Name");
                return View(pr);
            }
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Product pr)
        {
            try
            {
                db.Product.Update(pr);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                Product pr = db.Product.Find(id);
                if (pr == null)
                {
                    return NotFound();
                }
                db.Product.Remove(pr);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        public IActionResult OnGetGetVal()
        {
            return new JsonResult("Founded user");
        }

        [HttpPost]
        public JsonResult AjaxMethod([FromBody] Product product)
        {
            //pr.DateTime = DateTime.Now.ToString();
            return Json(product);
        }

        [HttpPost]
        public string GetValResult(string number1)
        {
            // Текущая дата
            string data = string.Empty;
            // Адрес сайта с курсом валюты
            string url = "https://benefit.by/currency/";
            // HTML сайта с курсом валюты
            string html = string.Empty;
            // Регулярное выражение
            string pattern = "1 Доллар США";
            DateTime today = DateTime.Now;
            data = today.Date.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);

            url += data;

            // Отправляем GET запрос и получаем в ответ HTML-код сайта с курсом валюты
            HttpWebRequest myHttpWebRequest = (HttpWebRequest)HttpWebRequest.Create(url);
            HttpWebResponse myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
            StreamReader myStreamReader = new StreamReader(myHttpWebResponse.GetResponseStream());
            html = myStreamReader.ReadToEnd();

            int x = html.LastIndexOf(pattern);
            int k = html.IndexOf("</tr>", x);
            string g = html.Substring(x, k - x);
            g = g.Replace("<td>", "").Replace("1 Доллар США", "").Replace("</td>", "").Replace("<br />", "").Replace(" ", "").Replace("@\r\n?|\n", "").Replace("\n", "");

            double r1 = double.Parse(g, CultureInfo.InvariantCulture);
            double r2 = Convert.ToDouble(number1);
            double result = r1 * r2;

            return "В долларах по курсу на " + today.Date.ToShortDateString() + " равен " + result.ToString() +" руб.";
            
        }
    }
}
