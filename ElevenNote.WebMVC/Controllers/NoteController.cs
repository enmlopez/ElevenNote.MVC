using ElevenNote.Models;
using ElevenNote.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ElevenNote.WebMVC.Controllers
{
    [Authorize]
    public class NoteController : Controller
    {
        // GET: Note
        public ActionResult Index()
        {
            //NoteListItem[] model = new NoteListItem[0];
            NoteService service = CreateNoteService();
            //Guid userId = Guid.Parse(User.Identity.GetUserId());
            //NoteService service = new NoteService(userId);
            IEnumerable<NoteListItem> model = service.GetNotes();

            return View(model);
        }

        //GET: Note/Create
        public ActionResult Create()
        {

            return View();
        }

        //POST: Note/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(NoteCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateNoteService();

            if (service.CreateNote(model))
            {
                TempData["SaveResult"] = "Your note was created.";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Note could not be created.");

            return View(model);

        }

        //GET: Details/{id}
        public ActionResult Details(int id)
        {
            NoteService service = CreateNoteService();
            NoteDetail model = service.GetNoteById(id);

            return View(model);
        }

        //GET: Edit/{id}
        public ActionResult Edit(int id)
        {
            NoteService service = CreateNoteService();
            NoteDetail detail = service.GetNoteById(id);
            NoteEdit model = new NoteEdit { NoteId = detail.NoteId, Title = detail.Title, Content = detail.Content };

            return View(model);
        }

        //POST: Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, NoteEdit model)
        {
            if (ModelState.IsValid)
            {
                return View(model);
            }

            if (model.NoteId != id)
            {
                ModelState.AddModelError("", "ID Mismatch");
                return View(model);
            }

            NoteService service = CreateNoteService();

            if (service.UpdateNote(model))
            {
                TempData["SaveResult"] = "Your note was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your note could not be updated.");
            return View();
        }

        //GET: Delete/{id}
        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            NoteService service = CreateNoteService();
            NoteDetail model = service.GetNoteById(id);

            return View(model);
        }

        //POST: Delete/{id}
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            NoteService service = CreateNoteService();
            service.DeleteNote(id);
            TempData["SaveResult"] = "Your note was deleted";

            return RedirectToAction("Index");
        }

        private NoteService CreateNoteService()
        {
            Guid userId = Guid.Parse(User.Identity.GetUserId());
            NoteService service = new NoteService(userId);
            return service;
        }
    }
}