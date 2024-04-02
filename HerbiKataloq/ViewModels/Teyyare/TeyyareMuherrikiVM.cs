using MongoDB.Bson.Serialization.Attributes;

namespace HerbiKataloq.ViewModels.Teyyare
{
    public class TeyyareMuherrikiVM
    {
        public string Modeli { get; set; } = null!;
        public string Istehsalcisi { get; set; } = null!;
        public double Gucu { get; set; }
    }
}
