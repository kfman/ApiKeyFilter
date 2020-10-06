using System.Collections.Generic;
using ApiKeyFilter.Models;

namespace ApiKeyFilter.Dto {
    public class ApiKeyDto : ModelBase {
        public string Key { get; set; }
        public string Description { get; set; }
        
        public IList<Role> Roles { get; set; }
    }
}
