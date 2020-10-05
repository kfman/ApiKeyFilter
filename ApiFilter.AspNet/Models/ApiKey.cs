using System.Collections.Generic;

namespace ApiKeyFilter {
    public class ApiKey {
        public string Key { get; set; }
        public string Description { get; set; }
        public List<Role> Roles { get; set; }
    }
}