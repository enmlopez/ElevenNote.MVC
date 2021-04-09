using ElevenNote.Models.CategoryModels;
using ElevenNote.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ElevenNote.WebMVC.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        // GET: Category
        public ActionResult Index()
        {
            var service = new CategoryService();
            var model = service.GetCategories();
            return View(model);
        }

        //GET: Category/Create
        public ActionResult Create()
        {
            return View();
        }

        //POST: Category/Create
        [HttpPost]
        public ActionResult Create(CategoryCreate model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var service = new CategoryService();

            if (service.CategoryCreate(model))
            {
                TempData["SaveResult"] = "Category Created";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Category could not be added");
            return View(model);
        }

        //GET: Category/Detail
        public ActionResult Details(int id)
        {
            var service = new CategoryService();
            var category = service.GetCategory(id);
            return View(category);
        }

        //GET: Category/Edit
        public ActionResult Edit(int id)
        {
            var service = new CategoryService();
            var detail = service.GetCategory(id);
            var category = new CategoryEdit() { CategoryName = detail.CategoryName };
            return View(category);
        }

        //POST: Category/Edit
        [HttpPost]
        public ActionResult Edit(int id, CategoryEdit model)
        {
            var service = new CategoryService();
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (service.UpdateCategory(id, model))
            {
                TempData["SaveResult"] = "Category Updated";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Category could not be updated.");
            return View(model);
        }

        //GET: Category/Delete
        public ActionResult Delete(int id)
        {
            var service = new CategoryService();
            var category = service.GetCategory(id);
            return View(category);
        }

        //POST: Category/Delete
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteNote(int id)
        {
            var service = new CategoryService();
            if (service.DeleteCategory(id))
            {
                TempData["SaveResult"] = "Category Deleted";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Category could not be updated");
            var model = service.GetCategory(id);
            return View(model);
        }
    }
}