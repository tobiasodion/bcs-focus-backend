using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BcsFocus.API.Models
{
    public class QuestionPoint
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = ObjectId.GenerateNewId().ToString();

        [BsonElement("questionDefinitions")]
        public string[]? QuestionDefinitions { get; set; }

        [BsonElement("notaBene")]
        public string NotaBene { get; set; } = String.Empty;

        [BsonElement("figure")]
        public Figure? Figure { get; set; }

        [BsonElement("meta")]
        public MetaQuestionPoint? Meta { get; set; }

        [BsonElement("mark")]
        public int Mark { get; set; }

        [BsonElement("subPoints")]
        public List<SubPoint>? SubPoints { get; set; }
        [BsonElement("uploadDate")]
        public DateTime UploadDate { get; set; }

        [BsonElement("modifyDate")]
        public DateTime ModifyDate { get; set; }
    }
}
