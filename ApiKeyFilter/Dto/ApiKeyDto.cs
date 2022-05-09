using System;
using System.Collections.Generic;

namespace ApiKeyFilter.Dto {
    public class ApiKeyDto {
        public string? Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Deleted { get; set; }
        public string Description { get; set; } = "";
        public IList<RoleDto> Roles { get; set; } = new List<RoleDto>();
    }
}
