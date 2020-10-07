using System.Collections.Generic;
using System.Linq;

namespace ApiKeyFilter.Models {
    public class ApiKey : ModelBase{
        public string Description { get; set; }
        public List<ApiKeyRoles> Roles { get; set; }

        public bool ContainsRoll(string role) => ContainsRoll(new List<string>{role});
        
        public bool ContainsRoll(IEnumerable<string> roles) {
            var owningRoles = Roles.Select(r => r.Role.Id).ToList();
            return roles.All(r => owningRoles.Contains(r));
        }
        
        
    }
}