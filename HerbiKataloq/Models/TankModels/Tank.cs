using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace HerbiKataloq.Models.TankModels
{
    [BsonIgnoreExtraElements]
    public class Tank
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = null!;

        [BsonElement("Adi")]
        [BsonRepresentation(BsonType.String)]
        [BsonIgnoreIfNull]
        public string Adi { get; set; } = null!;

        [BsonElement("Sinfi")]
        [BsonIgnoreIfDefault]
        public string Sinfi { get; set; } = null!;

        [BsonElement("Zirehi")]
        [BsonRepresentation(BsonType.String)]
        [BsonIgnoreIfNull]
        public string Zirehi { get; set; } = null!;

        [BsonElement("YanacaqTutumu(l)")]
        [BsonIgnoreIfDefault]
        public double YanacaqTutumu { get; set; }

        [BsonElement("PersonalSayi")]
        [BsonIgnoreIfDefault]
        public double PersonalSayi { get; set; }

        [BsonIgnoreIfNull]
        [BsonElement("Istehsalci")]
        public Istehsalci Istehsalci { get; set; } = null!;

        [BsonIgnoreIfNull]
        [BsonElement("Olculeri")]
        public Olcu Olculeri { get; set; } = null!;

        [BsonIgnoreIfNull]
        [BsonElement("Qabiliyyetleri")]
        public TankQabiliyyetleri Qabiliyyetleri { get; set; } = null!;

        [BsonIgnoreIfNull]
        [BsonElement("Muherriki")]
        public TankMuherriki Muherriki { get; set; } = null!;

        [BsonIgnoreIfNull]
        [BsonElement("Silahlari")]
        public List<TankSilahi> Silahlari { get; set; } = null!;

        [BsonElement("Foto")]
        [BsonRepresentation(BsonType.Binary)]
        [BsonIgnoreIfNull]
        public byte[] Foto { get; set; } = null!;

        [BsonIgnoreIfNull]
        [BsonElement("Qeyd")]
        public string Qeyd { get; set; } = null!;
    }
}
