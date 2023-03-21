using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BcsFocus.API.Models
{
    public class SubPoint
    {
        [BsonElement("questionDefinitions")]
        public string[]? QuestionDefinitions { get; set; }

        [BsonElement("mark")]
        public int Mark { get; set; }

        [BsonElement("meta")]
        public MetaQuestionPoint? Meta { get; set; }
    }
}