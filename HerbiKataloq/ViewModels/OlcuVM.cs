using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace HerbiKataloq.ViewModels
{
    public class OlcuVM
    {
        public double Uzunlugu { get; set; }
        public double Hundurluyu { get; set; }
        public double Eni { get; set; }
        public double Cekisi { get; set; }
    }
}
