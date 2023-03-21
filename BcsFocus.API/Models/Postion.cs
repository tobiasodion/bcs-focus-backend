using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BcsFocus.API.Models
{
    public class Position
    {
        [BsonElement("x")]
        public string X { get; set; } = String.Empty;

        [BsonElement("y")]
        public string Y { get; set; } = String.Empty;
    }
}
