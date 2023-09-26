﻿using GameZone.Data;
using GameZone.Models;
using GameZone.Settings;
using GameZone.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace GameZone.Services
{
    public class GamesService : IGamesService
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly string _imagesPath;

        public GamesService(ApplicationDbContext context,
            IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment; 
            _imagesPath = $"{_webHostEnvironment.WebRootPath}{FileSetting.ImagesPath}";
        }
        public IEnumerable<Game> GetAll()
        {
            return _context.Games
                     .Include(g => g.Category)
                     .Include(g => g.Device)
                     .ThenInclude(d => d.Device)
                     .AsNoTracking()
                     .ToList();
        }
        public async Task Create(CreateGameFormViewModel model)
        {
            var coverName = $"{Guid.NewGuid()}{Path.GetExtension(model.Cover.FileName)}";

            var path = Path.Combine(_imagesPath, coverName);

            using var stream = File.Create(path);
            await model.Cover.CopyToAsync(stream);

            Game game = new()
            {
                Name = model.Name,
                Description = model.Description,
                CategoryId = model.CategoryId,
                Cover = coverName,
                Device = model.SelectedDevices.Select(d => new GameDevice { DeviceId = d }).ToList()
            };

            _context.Add(game);
            _context.SaveChanges();
        }

        
    }
}
