using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace HerbiKataloq.Models.TeyyareModels
{
    [BsonIgnoreExtraElements]
    public class Teyyare
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = null!;

        [BsonElement("Adi")]
        [BsonRepresentation(BsonType.String)]
        [BsonIgnoreIfNull]
        public string Adi { get; set; } = null!;

        [BsonElement("Tipi")]
        [BsonRepresentation(BsonType.String)]
        [BsonIgnoreIfNull]
        public string Tipi { get; set; } = null!;

        [BsonElement("IlkUcusTarixi")]
        [BsonRepresentation(BsonType.DateTime)]
        [BsonIgnoreIfNull]
        [BsonIgnoreIfDefault]
        public DateTime IlkUcusTarixi { get; set; }

        [BsonElement("IstifadesiDavamEdirmi")]
        [BsonRepresentation(BsonType.Boolean)]
        public bool IstifadesiDavamEdirmi { get; set; }

        [BsonIgnoreIfNull]
        [BsonElement("Istehsalci")]
        public Istehsalci Istehsalci { get; set; } = null!;

        [BsonIgnoreIfNull]
        [BsonElement("Olculeri")]
        public Olcu Olculeri { get; set; } = null!;

        [BsonIgnoreIfNull]
        [BsonElement("Qabiliyyetleri")]
        public TeyyareQabiliyyetleri Qabiliyyetleri { get; set; } = null!;

        [BsonIgnoreIfNull]
        [BsonElement("Qeyd")]
        public string Qeyd { get; set; } = null!;

        [BsonIgnoreIfNull]
        [BsonElement("Muherriki")]
        public TeyyareMuherriki Muherriki { get; set; } = null!;

        [BsonIgnoreIfNull]
        [BsonElement("Silahlari")]
        public List<TeyyareSilahi> Silahlari { get; set; } = null!;

        [BsonElement("Foto")]
        [BsonRepresentation(BsonType.Binary)]
        [BsonIgnoreIfNull]
        public byte[] Foto { get; set; } = null!;
    }
}
