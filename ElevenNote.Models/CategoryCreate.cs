using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenNote.Models
{
    public class CategoryCreate
    {
        [Required]
        [MinLength(2, ErrorMessage = "Need more characters in this field.")]
        [MaxLength(50, ErrorMessage = "Name is too long.")]
        public string CatName { get; set; }
    }
}
