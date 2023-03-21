using System.Collections.ObjectModel;
using BcsFocus.API.Models;
using BcsFocus.API.Utils;
using Microsoft.VisualBasic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace BcsFocus.API.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly IMongoCollection<Question> _questions;
        private readonly IMongoCollection<Topic> _topics;
        private readonly IConfiguration _configuration;

        public QuestionService(IConfiguration configuration, IMongoClient mongoClient)
        {
            _configuration = configuration;
            var database = mongoClient.GetDatabase(_configuration["BcsStoreDbSettings:DatabaseName"]);
            _questions = database.GetCollection<Question>("questions");
            _topics = database.GetCollection<Topic>("topics");
        }

        public async Task<List<Question>> GetAll(string? topicId, int page, int limit, bool fraction)
        {
            var skip = (page - 1) * limit;
            var options = new FindOptions<Question>
            {
                Skip = skip,
                Limit = limit
            };

            var questionFilter = topicId == null ? Builders<Question>.Filter.Empty :
                                                  Builders<Question>.Filter.Empty &
                                                  Builders<Question>.Filter.AnyIn("topics", new BsonArray { topicId });

            var cursor = await _questions.FindAsync(questionFilter, options);
        
            var questions = await cursor.ToListAsync();

            if (questions != null)
            {
                var uniqueQuestions = questions.GroupBy(o => o.Id)
                                    .Select(g => g.First())
                                    .ToList();
                //if f is false return else return the transformed question
                if (fraction == false)
                {
                    return uniqueQuestions;
                }
                else
                {
                    var transformedQuestions = QuestionTransformation.transform(uniqueQuestions);
                    return transformedQuestions;
                }
            }else{
                return new List<Question>();
            }
        }

        public Question Get(string id)
        {
            return _questions.Find(question => question.Id == id).FirstOrDefault();
        }

        public async Task<Question> GetQuestionPoint(string id, string questionPointId)
        {
            var filter = Builders<Question>.Filter.And(
                         Builders<Question>.Filter.Eq(q => q.Id, id),
                         Builders<Question>.Filter.ElemMatch<Question>("questionPoints", Builders<Question>.Filter.Eq(p => p.Id, questionPointId))
                         );

            var projection = Builders<Question>.Projection
                .ElemMatch<Question>("questionPoints", Builders<Question>.Filter.Eq(p => p.Id, questionPointId))
                .Include(q => q.QuestionDefinitions)
                .Include(q => q.NotaBene)
                .Include(q => q.Figure)
                .Include(q => q.Meta)
                .Include(q => q.UploadDate)
                .Include(q => q.ModifyDate)
                .Include(q => q.Topics);

            var question = await _questions.Find(filter).Project<Question>(projection).FirstOrDefaultAsync();

            return question;
        }

        public Question Create(Question question)
        {
            question.SubParts = question.QuestionPoints != null ? question.QuestionPoints.Count() : 0;
           
            _questions.InsertOne(question);
            return question;
        }

        public void Update(string id, Question question)
        {
            question.SubParts = question.QuestionPoints != null ? question.QuestionPoints.Count() : 0;

            _questions.ReplaceOne(question => question.Id == id, question);
        }

        public void Remove(string id)
        {
            _questions.DeleteOne(question => question.Id == id);
        }

        public async Task<List<Topic>> GetAllTopics()
        {
            var questions = _questions.FindSync(new BsonDocument()).ToList();
            var topicIds = questions.SelectMany(m => m.Topics ?? Enumerable.Empty<string>()).Distinct().ToList();

            if (topicIds == null)
            {
                return new List<Topic>();
            }

            var filter = Builders<Topic>.Filter.In(t => t.Id, topicIds);

            var cursor = await _topics.FindAsync(filter);
            var topics = await cursor.ToListAsync();

            var uniqueTopics = topics.GroupBy(o => o.Id)
                                        .Select(g => g.First())
                                        .ToList();

            return uniqueTopics;
        }

        public async Task<List<Topic>> GetQuestionTopics(string questionId)
        {
            var filter = Builders<Question>.Filter.Eq(q => q.Id, questionId) & Builders<Question>.Filter.Exists(q => q.Topics);
            var question = _questions.Find(filter).FirstOrDefault();

            if (question != null && question.Topics != null)
            {
                var questionFilter = Builders<Topic>.Filter.In(t => t.Id, question.Topics);

                var cursor = await _topics.FindAsync(questionFilter);
                var topics = await cursor.ToListAsync();

                var uniqueTopics = topics.GroupBy(o => o.Id)
                                        .Select(g => g.First())
                                        .ToList();

                return uniqueTopics;
            }
            else
            {
                return new List<Topic>();
            }
        }
    }
}