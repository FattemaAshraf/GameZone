using GameZone.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GameZone.Services
{
    public class CategoriesService : ICategoriesService
    {
        private readonly ApplicationDbContext _context;

        public CategoriesService(ApplicationDbContext context)
        {
            _context = context;
        }
        public IEnumerable<SelectListItem> GetSelectList()
        {
            return _context.Categories
                .Select(C => new SelectListItem { Value = C.Id.ToString(), Text = C.Name })
                 .OrderBy(c => c.Text)
                 .ToList();
        }
    }
}
