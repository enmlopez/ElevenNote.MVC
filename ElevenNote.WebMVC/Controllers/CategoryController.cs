using ElevenNote.Models;
using ElevenNote.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ElevenNote.WebMVC.Controllers
{
    public class CategoryController : Controller
    {
        private CategoryService _service = new CategoryService();

        // GET: Category
        public ActionResult Index()
        {
            IEnumerable<CategoryListItem> model = _service.GetCategories();
            return View(model);
        }

        //GET: Create
        public ActionResult Create()
        {
            return View();
        }

        //POST: Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CategoryCreate model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (_service.CreateCategory(model))
            {
                TempData["SaveResult"] = "Your Category was created.";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Category could not be created.");

            return View(model);
        }
    }
}