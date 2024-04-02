using MongoDB.Bson.Serialization.Attributes;

namespace HerbiKataloq.Models.TankModels
{
    [BsonIgnoreExtraElements]
    public class TankSilahi
    {
        [BsonElement("Adi")]
        [BsonIgnoreIfNull]
        public string Adi { get; set; } = null!;

        [BsonElement("Teyinati")]
        [BsonIgnoreIfNull]
        public string Teyinati { get; set; } = null!;

        [BsonElement("Capi(mm)")]
        [BsonIgnoreIfDefault]
        public int Capi { get; set; }

        [BsonElement("AtisMesafesi(m)")]
        [BsonIgnoreIfDefault]
        public int AtisMesafesi { get; set; }
    }
}
