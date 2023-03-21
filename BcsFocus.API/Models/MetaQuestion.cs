using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BcsFocus.API.Models
{
    public class MetaQuestion
    {
        [BsonElement("section")]
        public string Section { get; set; } = String.Empty;

        [BsonElement("number")]
        public int Number { get; set; }

        [BsonElement("date")]
        public DateTime Date { get; set; }
    }
}
