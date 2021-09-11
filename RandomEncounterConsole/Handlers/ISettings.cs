using System.Collections.Generic;

namespace RandomEncounterConsole.Handlers
{
    public interface ISettings
    {
        public bool Exists(string name);
        public Setting Get(string name);
        public void Add(string name, string value, string description = null);
        public void Add(string name, int value, string description = null);
        public void Add(string name, double value, string description = null);
        public void Modify(string name, string value, bool addIfNotExists = false);
        public void Modify(string name, int value, bool addIfNotExists = false);
        public void Modify(string name, double value, bool addIfNotExists = false);
    }
}