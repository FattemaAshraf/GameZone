namespace GameZone.ViewModels
{
    public class CreateGameFormViewModel : GameFormViewModel
    {

        [AllowedExtentions(FileSetting.AllowedExtensions) ,
            MaxFileSize(FileSetting.MaxFileSizeInBytes)]
        public IFormFile Cover { get; set; } = default!;
    }
}

