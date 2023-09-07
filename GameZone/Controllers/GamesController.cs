using GameZone.Data;
using GameZone.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GameZone.Controllers
{
    public class GamesController : Controller
    {
        private readonly ApplicationDbContext _context;
        public GamesController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Create()
        {
            CreateGameFormViewModel viewModel = new()
            {
                //initailizing data from context to IEnumable<> Categories
                Categories = _context.Categories
                                     .Select(c => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = c.Id.ToString(), Text = c.Name })
                                     .OrderBy(c => c.Text)
                                     .ToList(),
                Devices = _context.Devices
                                     .Select(d => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = d.Id.ToString(), Text = d.Name })
                                     .OrderBy(d => d.Text)
                                     .ToList()
            };
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken] // for input _RequestVerficationToken to security form before submitted in database from attackers

        public IActionResult Create(CreateGameFormViewModel model)
        {
            //server side validation
            if (!ModelState.IsValid) 
            {
                return View();
            }

            //save game to database
            //save cover to server

            return RedirectToAction(nameof(Index));
        }
    }
}
