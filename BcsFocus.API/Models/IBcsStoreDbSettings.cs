namespace BcsFocus.API.Models
{
    public interface IBcsStoreDbSettings
    {
        string QuestionPointsCollectionName { get; set; }
        string ModuleTopicsCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}