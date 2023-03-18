using BcsFocus.API.Models;

namespace BcsFocus.API.Services
{
    public interface IModuleService{
        List<Module> Get();
        Module Get(string id);
        Module Create(Module module);
        void Update(string id, Module module);
        void Remove(string id);
        List<Module> GetByTopic(string t);
        Task<List<Topic>> GetAllTopics();
        Task<List<Topic>> GetModuleTopics(string moduleId);
        Task<List<Question>> GetModuleQuestions(string moduleId, string? topicId, int page, int limit, bool fraction);
    }
}