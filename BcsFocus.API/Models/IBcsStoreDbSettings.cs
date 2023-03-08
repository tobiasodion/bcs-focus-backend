namespace BcsFocus.API.Models
{
    public interface IBcsStoreDbSettings
    {
        string ModuleTopicsCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}