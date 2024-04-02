using MongoDB.Bson.Serialization.Attributes;

namespace HerbiKataloq.ViewModels.Teyyare
{
    public class TeyyareSilahiVM
    {
        public string TeyyareId { get; set; }
        public string Adi { get; set; } = null!;
        public string Teyinati { get; set; } = null!;
        public string Tipi { get; set; } = null!;
        public string XususiOzelliyi { get; set; } = null!;
    }
}
