using MongoDB.Bson.Serialization.Attributes;

namespace HerbiKataloq.Models
{
    [BsonIgnoreExtraElements]
    public class Istehsalci
    {
        [BsonElement("SirketinAdi")]
        [BsonIgnoreIfNull]
        public string SirketinAdi { get; set; } = null!;


        [BsonElement("Olke")]
        [BsonIgnoreIfNull]
        public string Olke { get; set; } = null!;
    }
}
