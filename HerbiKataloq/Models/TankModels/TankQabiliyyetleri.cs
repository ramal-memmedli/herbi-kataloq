using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace HerbiKataloq.Models.TankModels
{
    [BsonIgnoreExtraElements]
    public class TankQabiliyyetleri
    {
        [BsonElement("MuharibeRadiusu(km)")]
        [BsonRepresentation(BsonType.Double)]
        [BsonIgnoreIfDefault]
        public double MuharibeRadiusu { get; set; }

        [BsonElement("YoldaMaksimumSureti(km/s)")]
        [BsonRepresentation(BsonType.Double)]
        [BsonIgnoreIfDefault]
        public double YoldaMaksimumSureti { get; set; }

        [BsonElement("YolsuzluqSeraitindeMaksimumSureti(km/s)")]
        [BsonRepresentation(BsonType.Double)]
        [BsonIgnoreIfDefault]
        public double YolsuzluqSeraitindeMaksimumSureti { get; set; }
    }
}
