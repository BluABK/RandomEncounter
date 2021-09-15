using System;
using System.Collections.Generic;

namespace RandomEncounterConsole.Handlers
{
    public class Settings : ISettings
    {
        private readonly Dictionary<string, Setting> dict = new();

        public bool Exists(string name)
        {
            return dict.ContainsKey(name);
        }
        public Setting Get(string name)
        {
            return Exists(name) ? dict[name] : null;
        }

        public void Add(string name, string value, string description = null)
        {
            if (Exists(name)) throw new NotImplementedException("FIXME: Handle already exisiting setting name");

            dict.Add(name, new(name, value, description));
        }

        public void Add(string name, int value, string description = null)
        {
            if (Exists(name)) throw new NotImplementedException("FIXME: Handle already exisiting setting name");

            dict.Add(name, new(name, value, description));
        }

        public void Add(string name, double value, string description = null)
        {
            if (Exists(name)) throw new NotImplementedException("FIXME: Handle already exisiting setting name");

            dict.Add(name, new(name, value, description));
        }

        public void Modify(string name, string value, bool addIfNotExists = false)
        {
            if (!Exists(name) && !addIfNotExists) throw new ArgumentException($"Attempted to modify setting with name '{name}', but no such entry exists!");
            
            Get(name).ChangeTo<string>(value);

        }

        public void Modify(string name, int value, bool addIfNotExists = false)
        {
            if (!Exists(name) && !addIfNotExists) throw new ArgumentException($"Attempted to modify setting with name '{name}', but no such entry exists!");

            Get(name).ChangeTo<int>(value);
        }

        public void Modify(string name, double value, bool addIfNotExists = false)
        {
            if (!Exists(name) && !addIfNotExists) throw new ArgumentException($"Attempted to modify setting with name '{name}', but no such entry exists!");

            Get(name).ChangeTo<double>(value);
        }

        //public Dictionary<string,Setting> GetDict()
        //{
        //    return dict;
        //}

        public List<Setting> GetList()
        {
            List<Setting> list = new();
            foreach (Setting setting in dict.Values)
            {
                list.Add(setting);
            }

            return list;
        }
    }
}
