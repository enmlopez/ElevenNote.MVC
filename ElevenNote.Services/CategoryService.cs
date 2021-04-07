using ElevenNote.Data;
using ElevenNote.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenNote.Services
{
    public class CategoryService
    {
        public bool CreateCategory(CategoryCreate model)
        {
            Category entity = new Category() { CatName = model.CatName };

            using (ApplicationDbContext ctx = new ApplicationDbContext())
            {
                ctx.Categories.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<CategoryListItem> GetCategories()
        {
            using (ApplicationDbContext ctx = new ApplicationDbContext())
            {
                IEnumerable<CategoryListItem> query = ctx
                    .Categories
                    .Select(e => new CategoryListItem { CatId = e.CategoryId, CatName = e.CatName, CreatedUtc = e.CreatedUtc });

                return query.ToArray();
            }
        }
    }
}
