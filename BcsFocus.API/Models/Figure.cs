using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BcsFocus.API.Models
{
    public class Figure
    {
        [BsonElement("url")]
        public string Url { get; set; } = String.Empty;

        [BsonElement("position")]
        public Position? Position { get; set; }

        [BsonElement("caption")]
        public Caption? Caption { get; set; }
    }
}
