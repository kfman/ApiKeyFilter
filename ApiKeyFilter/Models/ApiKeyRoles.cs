namespace ApiKeyFilter.Models {
    public class ApiKeyRoles {
        public string ApiKeyId { get; set; } = null!;
        public ApiKey ApiKey { get; set; } = null!;
        public string RoleId { get; set; } = null!;
        public Role Role { get; set; } = null!;
    }
}
