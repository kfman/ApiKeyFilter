using System.Collections.Generic;

namespace ApiKeyFilter.Models {
    public class ApiKey : ModelBase{
        public string Key { get; set; }
        public string Description { get; set; }
        public List<Role> Roles { get; set; }
    }
}