using BcsFocus.API.Models;

namespace BcsFocus.API.Services
{
    public interface IModuleService{
        List<Module> Get();
        Module Get(string id);
        Module Create(Module module);
        void Update(string id, Module module);
        void Remove(string id);
    }
}