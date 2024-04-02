using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace HerbiKataloq.Models.TeyyareModels
{
    [BsonIgnoreExtraElements]
    public class TeyyareQabiliyyetleri
    {
        [BsonElement("MaksimumSureti(Mach)")]
        [BsonRepresentation(BsonType.Double)]
        [BsonIgnoreIfDefault]
        public double MaksimumSureti { get; set; }


        [BsonElement("MaksimumUcusHundurluyu(m)")]
        [BsonRepresentation(BsonType.Double)]
        [BsonIgnoreIfDefault]
        public double MaksimumUcusHundurluyu { get; set; }
    }
}
