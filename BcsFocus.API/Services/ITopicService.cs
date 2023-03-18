using BcsFocus.API.Models;

namespace BcsFocus.API.Services
{
    public interface ITopicService{
        List<Topic> Get();
        Topic Get(string id);
        Topic Create(Topic module);
        void Update(string id, Topic module);
        void Remove(string id);
    }
}