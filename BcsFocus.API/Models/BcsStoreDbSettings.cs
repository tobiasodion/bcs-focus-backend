namespace BcsFocus.API.Models
{
    public class BcsStoreDbSettings : IBcsStoreDbSettings {
        public string ModuleTopicsCollectionName { get; set; } = String.Empty;
        public string ConnectionString { get; set; } = String.Empty;
        public string DatabaseName { get; set; } = String.Empty;
    }
}