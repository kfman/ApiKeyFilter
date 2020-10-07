using System.Collections;
using System.Collections.Generic;

namespace ApiKeyFilter.Models {
    public class Role: ModelBase {
        public IList<ApiKeyRoles> ApiKeys { get; set; }
    }
}