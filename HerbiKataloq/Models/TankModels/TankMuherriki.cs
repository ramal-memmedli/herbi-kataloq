using MongoDB.Bson.Serialization.Attributes;

namespace HerbiKataloq.Models.TankModels
{
    [BsonIgnoreExtraElements]
    public class TankMuherriki
    {
        [BsonElement("Modeli")]
        [BsonIgnoreIfNull]
        public string Modeli { get; set; } = null!;

        [BsonElement("Istehsalcisi")]
        [BsonIgnoreIfNull]
        public string Istehsalcisi { get; set; } = null!;

        [BsonElement("YanacaqNovu")]
        [BsonIgnoreIfNull]
        public string YanacaqNovu { get; set; } = null!;

        [BsonElement("Gucu(hp)")]
        [BsonIgnoreIfDefault]
        public double Gucu { get; set; }
    }
}
