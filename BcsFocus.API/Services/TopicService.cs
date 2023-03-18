using BcsFocus.API.Models;
using MongoDB.Driver;

namespace BcsFocus.API.Services
{
    public class TopicService : ITopicService{
        private readonly IMongoCollection<Topic> _topics;
        private readonly IConfiguration _configuration;

        public TopicService(IConfiguration configuration, IMongoClient mongoClient){
            _configuration = configuration;
            var database = mongoClient.GetDatabase(_configuration["BcsStoreDbSettings:DatabaseName"]);
            _topics = database.GetCollection<Topic>("topics");
        }

        List<Topic> ITopicService.Get()
        {
            return _topics.Find(topic => true).ToList();
        }

        Topic ITopicService.Get(string id)
        {
           return _topics.Find(topic => topic.Id == id).FirstOrDefault();
        }

        Topic ITopicService.Create(Topic topic)
        {
            _topics.InsertOne(topic);
            return topic;
        }

        void ITopicService.Update(string id, Topic topic)
        {
            _topics.ReplaceOne(topic => topic.Id == id, topic);
        }

        void ITopicService.Remove(string id)
        {
            _topics.DeleteOne(topic => topic.Id == id );
        }
    }
}