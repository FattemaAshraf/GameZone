using GameZone.ViewModels;

namespace GameZone.Services
{
    public interface IGamesService
    {
        Task Create(CreateGameFormViewModel model);

    }
}
