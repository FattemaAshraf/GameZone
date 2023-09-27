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
        private readonly IGamesService _gamesServices;

        public GamesController(ICategoriesService categoriesService,
            IDevicesService devicesService,
            IGamesService gamesServices)
        {
            _categoriesService = categoriesService;
            _devicesService = devicesService;
            _gamesServices = gamesServices;
        }
        public IActionResult Index()
        {
            var games = _gamesServices.GetAll();
            return View(games);
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
            await _gamesServices.Create(model);


            return RedirectToAction(nameof(Index));
        }
    }
}
