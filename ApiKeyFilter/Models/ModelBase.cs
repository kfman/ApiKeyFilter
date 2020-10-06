using System;

namespace ApiKeyFilter.Models {
    public class ModelBase {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Deleted { get; set; }
    }
}