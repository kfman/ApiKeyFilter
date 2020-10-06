using System.Collections.Generic;

namespace ApiKeyFilter.Models {
    public class ApiKey : ModelBase{
        public string Key { get; set; }
        public string Description { get; set; }
        public List<ApiKeyRoles> Roles { get; set; }
    }

    public class ApiKeyRoles {
        public int ApiKeyId { get; set; }
        public ApiKey ApiKey { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
    }
}