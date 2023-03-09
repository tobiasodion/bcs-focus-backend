using BcsFocus.API.Models;
using MongoDB.Driver;

namespace BcsFocus.API.Services
{
    public class QuestionService : IQuestionService{
        private readonly IMongoCollection<Question> _questions;
        private readonly IConfiguration _configuration;

        public QuestionService(IConfiguration configuration, IMongoClient mongoClient){
            _configuration = configuration;
            var database = mongoClient.GetDatabase(_configuration["BcsStoreDbSettings:DatabaseName"]);
            _questions = database.GetCollection<Question>("questionpoints");
        }

        List<Question> IQuestionService.Get()
        {
            return _questions.Find(question => true).ToList();
        }

        Question IQuestionService.Get(string id)
        {
           return _questions.Find(question => question.Id == id).FirstOrDefault();
        }

        Question IQuestionService.Create(Question question)
        {
            _questions.InsertOne(question);
            return question;
        }

        void IQuestionService.Update(string id, Question question)
        {
            _questions.ReplaceOne(question => question.Id == id, question);
        }

        void IQuestionService.Remove(string id)
        {
            _questions.DeleteOne(question => question.Id == id );
        }
    }
}