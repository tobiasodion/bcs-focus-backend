using BcsFocus.API.Models;

namespace BcsFocus.API.Services
{
    public interface IQuestionService{
        List<Question> Get();
        Question Get(string id);
        Question Create(Question module);
        void Update(string id, Question module);
        void Remove(string id);
    }
}