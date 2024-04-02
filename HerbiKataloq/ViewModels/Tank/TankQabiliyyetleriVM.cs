using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace HerbiKataloq.ViewModels.Tank
{
    public class TankQabiliyyetleriVM
    {
        public double MuharibeRadiusu { get; set; }
        public double YoldaMaksimumSureti { get; set; }
        public double YolsuzluqSeraitindeMaksimumSureti { get; set; }
    }
}
