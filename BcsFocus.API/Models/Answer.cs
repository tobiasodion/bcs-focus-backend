using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BcsFocus.API.Models
{
    public class Answer
    {
        [BsonElement("answerDefinitions")]
        public string[]? AnswerDefinitions { get; set; }

        [BsonElement("meta")]
        public MetaQuestion? Meta { get; set; }

        [BsonElement("notaBene")]
        public string NotaBene { get; set; } = String.Empty;

        [BsonElement("extraReferences")]
        public string[]? ExtraReferences { get; set; }

        [BsonElement("figure")]
        public Figure? Figure { get; set; }

        [BsonElement("uploadDate")]
        public DateTime UploadDate { get; set; }

        [BsonElement("modifyDate")]
        public DateTime ModifyDate { get; set; }
    }
}
