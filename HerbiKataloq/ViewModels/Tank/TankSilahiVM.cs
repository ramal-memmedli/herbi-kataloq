using MongoDB.Bson.Serialization.Attributes;

namespace HerbiKataloq.ViewModels.Tank
{
    public class TankSilahiVM
    {
        public string TankId { get; set; }
        public string Adi { get; set; } = null!;
        public string Teyinati { get; set; } = null!;
        public int Capi { get; set; }
        public int AtisMesafesi { get; set; }
    }
}
