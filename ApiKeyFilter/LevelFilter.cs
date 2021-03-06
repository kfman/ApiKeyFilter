using System;

namespace ApiKeyFilter {
    public class LevelFilter : Attribute {
        public const string MasterKeyOnly = "MasterKeyOnly";
        public const string AllKeysAllowed = "JustExisting";
        public string Level { get; }

        public LevelFilter(string level) {
            Level = level;
        }
    }
}