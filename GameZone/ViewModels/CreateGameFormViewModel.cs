using GameZone.Attributes;
using GameZone.Settings;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace GameZone.ViewModels
{
    public class CreateGameFormViewModel : GameFormViewModel
    {

        [AllowedExtentions(FileSetting.AllowedExtensions) ,
            MaxFileSize(FileSetting.MaxFileSizeInBytes)]
        public IFormFile Cover { get; set; } = default!;
    }
}

