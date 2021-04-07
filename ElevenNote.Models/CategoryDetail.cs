using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenNote.Models
{
    public class CategoryDetail
    {
        [Display(Name ="ID")]
        public int CatId { get; set; }

        [Display(Name ="Name")]
        public string CatName { get; set; }

        public List<NoteListItem> ListOfNotes { get; set; }

        [Display(Name ="Created")]
        public DateTimeOffset CreatedUtc { get; set; }


        [Display(Name ="Modified")]
        public DateTimeOffset? ModifiedUtc { get; set; }

    }
}
