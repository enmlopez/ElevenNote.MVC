using System.Collections.Generic;

namespace ElevenNote.Models.CategoryModels
{
    public class CategoryDetail
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public List<NoteListItem> Notes { get; set; } = new List<NoteListItem>();
    }
}
