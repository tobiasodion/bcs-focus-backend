using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

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
        public MetaData? Meta { get; set; }

        [BsonElement("uploadDate")]
        public DateTime UploadDate { get; set; }

        [BsonElement("modifyDate")]
        public DateTime ModifyDate { get; set; }

        [BsonElement("topics")]
        public string[]? Topics { get; set; }

        [BsonElement("questionPoints")]
        public List<QuestionPoint>? QuestionPoints { get; set; }
    }

    public class Figure
    {
        [BsonElement("url")]
        public string Url { get; set; } = String.Empty;

        [BsonElement("position")]
        public Position? Position { get; set; }

        [BsonElement("caption")]
        public Caption? Caption { get; set; }
    }

    public class Position
    {
        [BsonElement("x")]
        public string X { get; set; } = String.Empty;

        [BsonElement("y")]
        public string Y { get; set; } = String.Empty;
    }

    public class Caption
    {
        [BsonElement("title")]
        public string Title { get; set; } = String.Empty;

        [BsonElement("position")]
        public string Position { get; set; } = String.Empty;
    }

    public class MetaData
    {
        [BsonElement("section")]
        public string Section { get; set; } = String.Empty;

        [BsonElement("number")]
        public int Number { get; set; }

        [BsonElement("date")]
        public DateTime Date { get; set; }

        [BsonElement("type")]
        public string Type { get; set; } = String.Empty;
    }

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
        public MetaData? Meta { get; set; }

        [BsonElement("mark")]
        public int Mark { get; set; }

        [BsonElement("subPoints")]
        public string[]? SubPoints { get; set; }
    }
}
