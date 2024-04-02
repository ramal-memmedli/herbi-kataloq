using MongoDB.Bson.Serialization.Attributes;

namespace HerbiKataloq.Models.TeyyareModels
{
    [BsonIgnoreExtraElements]
    public class TeyyareMuherriki
    {
        [BsonElement("Modeli")]
        [BsonIgnoreIfNull]
        public string Modeli { get; set; } = null!;


        [BsonElement("Istehsalcisi")]
        [BsonIgnoreIfNull]
        public string Istehsalcisi { get; set; } = null!;


        [BsonElement("Gucu(kN)")]
        [BsonIgnoreIfDefault]
        public double Gucu { get; set; }
    }
}
