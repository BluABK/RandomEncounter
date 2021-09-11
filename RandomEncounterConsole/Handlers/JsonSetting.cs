using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RandomEncounterConsole.Handlers
{
    class JsonSetting<T>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public T Value { get; set; }

        public JsonSetting(string name, T value, string description)
        {
            Name = name;
            Value = value;
            Description = description;
        }
    }

    class JsonSettingString
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Value { get; set; }
    }

    class JsonSettingInt
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Value { get; set; }
    }

    class JsonSettingDouble
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Value { get; set; }
    }
}
