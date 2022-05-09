namespace ApiKeyFilter.Models {
    public class LogEntry : ModelBase {
        public string ApiKeyString { get; set; } = "";
        public string Controller { get; set; } = "";
        public bool AccessGranted { get; set; }
    }
}
