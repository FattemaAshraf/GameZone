using GameZone.Data;
using GameZone.Services;
using GameZone.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GameZone.Controllers
{
    public class GamesController : Controller
    {
        private readonly ICategoriesService _categoriesService;
        private readonly IDevicesService _devicesService;
        private readonly IGamesService _gamesService;

        public GamesController(ICategoriesService categoriesService,
            IDevicesService devicesService,
            IGamesService gamesService)
        {
            _categoriesService = categoriesService;
            _devicesService = devicesService;
            _gamesService = gamesService;
        }
        public IActionResult Index()
        {
            var games = _gamesService.GetAll();
            return View(games);
        }

        public IActionResult Details(int id)
        {
            var game = _gamesService.GetById(id);

            if (game is null)
                return NotFound();

            return View(game);
        }

        [HttpGet]
        public IActionResult Create()
        {
            CreateGameFormViewModel viewModel = new()
            {
                //initailizing data from context to IEnumable<> Categories
                Categories = _categoriesService.GetSelectList(),
                Devices = _devicesService.GetSelectList()
            };
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken] // for input _RequestVerficationToken to security form before submitted in database from attackers

        public async Task<IActionResult> Create(CreateGameFormViewModel model)
        {
            //server side validation
            if (!ModelState.IsValid) 
            {
                model.Categories = _categoriesService.GetSelectList();
                model.Devices = _devicesService.GetSelectList();
                return View(model);
            }

            //save game to database
            //save cover to server
            await _gamesService.Create(model);


            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditGameFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = _categoriesService.GetSelectList();
                model.Devices = _devicesService.GetSelectList();
                return View(model);
            }

            var game = await _gamesService.Update(model);

            if (game is null)
                return BadRequest();

            return RedirectToAction(nameof(Index));
        }
    }
}
