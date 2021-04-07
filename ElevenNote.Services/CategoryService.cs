using ElevenNote.Data;
using ElevenNote.Models.CategoryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenNote.Services
{
    public class CategoryService
    {
        public bool CategoryCreate(CategoryCreate model)
        {
            Category entity = new Category() { Name = model.CatName };

            using (ApplicationDbContext ctx = new ApplicationDbContext())
            {
                ctx.Categories.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public List<CategoryListItem> GetCategories()
        {
            using (ApplicationDbContext ctx = new ApplicationDbContext())
            {
                return ctx.Categories.Select(e => new CategoryListItem { CategoryName = e.Name, CategoryId = e.CategoryId }).ToList();
            }
        }

        public CategoryDetail GetCategory(int id)
        {
            using (ApplicationDbContext ctx = new ApplicationDbContext())
            {
                var entity = ctx.Categories.SingleOrDefault(e => e.CategoryId == id);
                var category = new CategoryDetail
                {
                    CategoryId = entity.CategoryId,
                    CategoryName = entity.Name,
                    Notes = entity.Notes.Select(e => new Models.NoteListItem
                    {
                        Title = e.Title,
                        Category = e.Category.CategoryId + " " + e.Category.Name,
                        CreatedUtc = e.CreatedUtc,
                        NoteId = e.NoteId
                    }).ToList()
                };
                return category;
            }
        }

        public bool UpdateCategory(int id, CategoryEdit model)
        {
            using (ApplicationDbContext ctx = new ApplicationDbContext())
            {
                var entity = ctx.Categories.SingleOrDefault(e => e.CategoryId == id);
                entity.Name = model.CategoryName;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteCategory(int id)
        {
            using (ApplicationDbContext ctx = new ApplicationDbContext())
            {
                var entity = ctx.Categories.SingleOrDefault(e => e.CategoryId == id);
                ctx.Categories.Remove(entity);
                return ctx.SaveChanges() == 1;

            }
        }
    }
}
