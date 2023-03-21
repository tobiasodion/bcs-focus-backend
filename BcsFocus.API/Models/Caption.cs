using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BcsFocus.API.Models
{
    public class Caption
    {
        [BsonElement("title")]
        public string Title { get; set; } = String.Empty;

        [BsonElement("position")]
        public string Position { get; set; } = String.Empty;
    }
}
