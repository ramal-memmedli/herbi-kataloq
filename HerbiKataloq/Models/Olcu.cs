using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace HerbiKataloq.Models
{
    [BsonIgnoreExtraElements]
    public class Olcu
    {
        [BsonElement("Uzunlugu(m)")]
        [BsonRepresentation(BsonType.Double)]
        [BsonIgnoreIfDefault]
        public double Uzunlugu { get; set; }


        [BsonElement("Hundurluyu(m)")]
        [BsonRepresentation(BsonType.Double)]
        [BsonIgnoreIfDefault]
        public double Hundurluyu { get; set; }


        [BsonElement("Eni(m)")]
        [BsonRepresentation(BsonType.Double)]
        [BsonIgnoreIfDefault]
        public double Eni { get; set; }


        [BsonElement("Cekisi(Kg)")]
        [BsonRepresentation(BsonType.Double)]
        [BsonIgnoreIfDefault]
        public double Cekisi { get; set; }
    }
}
