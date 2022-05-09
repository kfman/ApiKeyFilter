using System;

namespace ApiKeyFilter.Models {
    public class ModelBase {
        public string Id { get; set; } = null!;
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime? Deleted { get; set; }
    }
}
