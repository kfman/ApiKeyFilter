using System;
using System.Collections.Generic;
using ApiKeyFilter.Models;

namespace ApiKeyFilter.Dto {
    public class ApiKeyDto {
        public string Id { get; set; }
        public DateTime Created { get; set; }
        public string Description { get; set; }
        public IList<RoleDto> Roles { get; set; }
    }

    public class RoleDto  {
        public string Id { get; set; }
        public DateTime Created { get; set; }
    }
}
