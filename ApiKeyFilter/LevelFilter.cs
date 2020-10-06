using System;

namespace ApiKeyFilter {
    public class LevelFilter : Attribute {
        public string Level { get; }

        public LevelFilter(string level) {
            Level = level;
        }
    }
}