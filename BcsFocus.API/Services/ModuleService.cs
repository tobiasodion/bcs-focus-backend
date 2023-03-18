using BcsFocus.API.Models;
using BcsFocus.API.Utils;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BcsFocus.API.Services
{
    public class ModuleService : IModuleService
    {
        private readonly IMongoCollection<Module> _modules;
        private readonly IMongoCollection<Topic> _topics;
        private readonly IMongoCollection<Question> _questions;
        private readonly IConfiguration _configuration;

        public ModuleService(IConfiguration configuration, IMongoClient mongoClient)
        {
            _configuration = configuration;
            var database = mongoClient.GetDatabase(_configuration["BcsStoreDbSettings:DatabaseName"]);
            _modules = database.GetCollection<Module>("modules");
            _topics = database.GetCollection<Topic>("topics");
            _questions = database.GetCollection<Question>("questions");
        }

        public List<Module> Get()
        {
            return _modules.Find(module => true).ToList();
        }

        public Module Get(string id)
        {
            return _modules.Find(module => module.Id == id).FirstOrDefault();
        }

        public Module Create(Module module)
        {
            _modules.InsertOne(module);
            return module;
        }

        public void Update(string id, Module module)
        {
            _modules.ReplaceOne(module => module.Id == id, module);
        }

        public void Remove(string id)
        {
            _modules.DeleteOne(module => module.Id == id);
        }

        public List<Module> GetByTopic(string topicId)
        {
            var filter = Builders<Module>.Filter.AnyEq("topics", topicId);
            var modulesByTopic = _modules.Find(filter).ToList();
            return modulesByTopic;
        }

        public async Task<List<Topic>> GetAllTopics()
        {
            var modules = _modules.FindSync(new BsonDocument()).ToList();
            var topicIds = modules.SelectMany(m => m.Topics ?? Enumerable.Empty<string>()).Distinct().ToList();

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

        public async Task<List<Topic>> GetModuleTopics(string moduleId)
        {
            var filter = Builders<Module>.Filter.Eq(m => m.Id, moduleId) & Builders<Module>.Filter.Exists(m => m.Topics);
            var module = _modules.Find(filter).FirstOrDefault();

            if (module != null && module.Topics != null)
            {
                var topicFilter = Builders<Topic>.Filter.In(t => t.Id, module.Topics);

                var cursor = await _topics.FindAsync(topicFilter);
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

        public async Task<List<Question>> GetModuleQuestions(string moduleId, string? topicId, int page, int limit, bool fraction)
        {
            var skip = (page - 1) * limit;
            var options = new FindOptions<Question>
            {
                Skip = skip,
                Limit = limit
            };

            var filter = Builders<Module>.Filter.Eq(m => m.Id, moduleId) & Builders<Module>.Filter.Exists(m => m.Questions);
            var module = _modules.Find(filter).FirstOrDefault();

            if (module != null && module.Questions != null)
            {
                var questionFilter = topicId == null ? Builders<Question>.Filter.In(q => q.Id, module.Questions) :
                                                      Builders<Question>.Filter.In(q => q.Id, module.Questions) &
                                                      Builders<Question>.Filter.AnyIn("topics", new BsonArray { topicId });

                var cursor = await _questions.FindAsync(questionFilter, options);
                
                var questions = await cursor.ToListAsync();
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

            }
            else
            {
                return new List<Question>();
            }
        }
    }
}