namespace ApiKeyFilter.Models {
    public class ApiKeyRoles {
        public string ApiKeyId { get; set; }
        public ApiKey ApiKey { get; set; }
        public string RoleId { get; set; }
        public Role Role { get; set; }
    }
}