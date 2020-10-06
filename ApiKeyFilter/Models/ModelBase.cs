using System;

namespace ApiKeyFilter.Models {
    public class ModelBase {
        public int Id { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime? Deleted { get; set; }
    }
}