using MongoDB.Bson.Serialization.Attributes;

namespace HerbiKataloq.ViewModels.Tank
{
    public class TankMuherrikiVM
    {
        public string Modeli { get; set; } = null!;
        public string Istehsalcisi { get; set; } = null!;
        public string YanacaqNovu { get; set; } = null!;
        public double Gucu { get; set; }
    }
}
