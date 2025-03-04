namespace MessageService.Common.Configuration
{
    public class ServiceConfig
    {
        public string Assembly { get; set; }
        public string Connection { get; set; }
        public string CloudStorageConnStr { get; set; }
        public string PrivateCloudStorageConnStr { get; set; }
        public bool Active { get; set; }
        public string SourceUnitType { get; set; }
        public string DestUnitType { get; set; }
        public bool IsDevEnvironment { get; set; } 
    }
}
