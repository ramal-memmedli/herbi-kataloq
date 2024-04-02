using HerbiKataloq.Models.TankModels;

namespace HerbiKataloq.Services.TankServices
{
    public interface ITankService : IBaseService<Tank>
    {
        Task AddWeapon(string id, TankSilahi uEntity);
        Task RemoveWeapon(string id, TankSilahi uEntity);
    }
}
