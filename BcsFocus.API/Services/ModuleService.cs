using BcsFocus.API.Models;
using MongoDB.Driver;

namespace BcsFocus.API.Services
{
    public class ModuleService : IModuleService{
        private readonly IMongoCollection<Module> _modules;

        public ModuleService(IBcsStoreDbSettings settings, IMongoClient mongoClient){
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _modules = database.GetCollection<Module>(settings.ModuleTopicsCollectionName);
        }

        List<Module> IModuleService.Get()
        {
            return _modules.Find(module => true).ToList();
        }

        Module IModuleService.Get(string id)
        {
           return _modules.Find(module => module.Id == id).FirstOrDefault();
        }

        Module IModuleService.Create(Module module)
        {
            _modules.InsertOne(module);
            return module;
        }

        void IModuleService.Update(string id, Module module)
        {
            _modules.ReplaceOne(module => module.Id == id, module);
        }

        void IModuleService.Remove(string id)
        {
            _modules.DeleteOne(module => module.Id == id );
        }
    }
}