using BcsFocus.API.Models;

namespace BcsFocus.API.Services
{
    public interface IQuestionService{
        Task<List<Question>> GetAll(string? topicId, int page, int limit, bool fraction);
        Question Get(string id);
        Task<Question> GetQuestionPoint(string id, string questionPointId);
        Question Create(Question module);
        void Update(string id, Question module);
        void Remove(string id);
        Task<List<Topic>> GetAllTopics();
        Task<List<Topic>> GetQuestionTopics(string questionId);
    }
}