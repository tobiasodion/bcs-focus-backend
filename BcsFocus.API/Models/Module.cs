using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BcsFocus.API.Models
{
    public class Module
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = String.Empty;

        [BsonElement("title")]
        public string Title { get; set; } = String.Empty;

        [BsonElement("description")]
        public string Description { get; set; } = String.Empty;

        [BsonElement("createdDate")]
        public string CreatedDate { get; set; } = String.Empty;

        [BsonElement("modifyDate")]
        public string ModifyDate { get; set; } = String.Empty;

        [BsonElement("topics")]
        public string[]? Topics { get; set; }

        [BsonElement("questions")]
        public string[]? Questions { get; set; }
    }
}