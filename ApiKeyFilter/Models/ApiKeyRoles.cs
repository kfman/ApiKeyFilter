namespace ApiKeyFilter.Models {
    public class ApiKeyRoles {
        public int ApiKeyId { get; set; }
        public ApiKey ApiKey { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
    }
}