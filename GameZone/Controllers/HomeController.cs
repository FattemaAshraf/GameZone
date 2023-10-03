using GameZone.Models;
using GameZone.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GameZone.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICategoriesService _categoriesService;
        private readonly IDevicesService _devicesService;
        private readonly IGamesService _gamesService;

        public HomeController(ICategoriesService categoriesService,
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

        public IActionResult Search(string searchString)
        {
            var games = _gamesService.Search(searchString);

            return View(games);

        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}