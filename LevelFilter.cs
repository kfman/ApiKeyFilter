using System;

namespace ApiKeyFilter {
    public class LevelFilter : Attribute {
        public int Level { get; }

        public LevelFilter(int level) {
            Level = level;
        }
    }
}