using MongoDB.Bson.Serialization.Attributes;

namespace HerbiKataloq.Models.TeyyareModels
{
    [BsonIgnoreExtraElements]
    public class TeyyareSilahi
    {
        [BsonElement("Adi")]
        [BsonIgnoreIfNull]
        public string Adi { get; set; } = null!;

        [BsonElement("Teyinati")]
        [BsonIgnoreIfNull]
        public string Teyinati { get; set; } = null!;

        [BsonElement("Tipi")]
        [BsonIgnoreIfNull]
        public string Tipi { get; set; } = null!;

        [BsonElement("XususiOzelliyi")]
        [BsonIgnoreIfNull]
        public string XususiOzelliyi { get; set; } = null!;
    }
}
