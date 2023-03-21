using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BcsFocus.API.Models
{
    public class MetaQuestionPoint
    {
        [BsonElement("number")]
        public int Number { get; set; }
        [BsonElement("type")]
        public string Type { get; set; } = String.Empty;
    }
}
