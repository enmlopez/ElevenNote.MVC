using ElevenNote.Data;
using ElevenNote.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenNote.Services
{
    public class NoteService
    {
        private readonly Guid _userId;

        public NoteService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateNote(NoteCreate model)
        {
            Note entity = new Note() { OwnerId = _userId, Title = model.Title, Content = model.Content, CreatedUtc = DateTimeOffset.Now };

            using (ApplicationDbContext ctx = new ApplicationDbContext())
            {
                ctx.Notes.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<NoteListItem> GetNotes()
        {
            using (ApplicationDbContext ctx = new ApplicationDbContext())
            {
                IEnumerable<NoteListItem> query = ctx
                    .Notes
                    .Where(e => e.OwnerId == _userId)
                    .Select(e => new NoteListItem { NoteId = e.NoteId, Title = e.Title, CreatedUtc = e.CreatedUtc });
                return query.ToArray();
            }
        }

        public NoteDetail GetNoteById(int id)
        {
            using (ApplicationDbContext ctx = new ApplicationDbContext())
            {
                Note entity = ctx.Notes.SingleOrDefault(e => e.NoteId == id && e.OwnerId == _userId);
                return new NoteDetail { NoteId = entity.NoteId, Content = entity.Content, Title = entity.Title, CreatedUtc = entity.CreatedUtc, ModifiedUtc = entity.ModifiedUtc };
            }
        }

        public bool UpdateNote(NoteEdit model)
        {
            using (ApplicationDbContext ctx = new ApplicationDbContext())
            {
                Note entity = ctx.Notes.SingleOrDefault(e => e.NoteId == model.NoteId && e.OwnerId == _userId);
                entity.Title = model.Title;
                entity.Content = model.Content;
                entity.ModifiedUtc = DateTimeOffset.UtcNow;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteNote(int noteId)
        {
            using (ApplicationDbContext ctx = new ApplicationDbContext())
            {
                Note entity = ctx.Notes.SingleOrDefault(e => e.NoteId == noteId && e.OwnerId == _userId);

                ctx.Notes.Remove(entity);

                return ctx.SaveChanges() == 1;
            }

        }
    }
}
