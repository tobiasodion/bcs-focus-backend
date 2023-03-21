using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BcsFocus.API.Models
{
    public class Question
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = String.Empty;

        [BsonElement("questionDefinitions")]
        public string[]? QuestionDefinitions { get; set; }

        [BsonElement("notaBene")]
        public string NotaBene { get; set; } = String.Empty;

        [BsonElement("figure")]
        public Figure? Figure { get; set; }

        [BsonElement("meta")]
        public MetaQuestion? Meta { get; set; }

        [BsonElement("subParts")]
        public int SubParts { get; set; }

        [BsonElement("uploadDate")]
        public DateTime UploadDate { get; set; }

        [BsonElement("modifyDate")]
        public DateTime ModifyDate { get; set; }

        [BsonElement("topics")]
        public string[]? Topics { get; set; }

        [BsonElement("questionPoints")]
        public List<QuestionPoint>? QuestionPoints { get; set; }

        [BsonElement("answer")]
        public Answer? Answer { get; set; }
    }
}
