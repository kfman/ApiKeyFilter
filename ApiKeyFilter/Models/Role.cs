using System.Collections;
using System.Collections.Generic;

namespace ApiKeyFilter.Models {
    public class Role: ModelBase {
        public string Name { get; set; }
        public IList<ApiKeyRoles> ApiKeys { get; set; }
    }
}