using System;

namespace RandomEncounterConsole.Handlers
{
    public class Setting : ISetting
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public ESettingType Type { get; private set; }
        public string StringValue { get; private set; }
        public int? IntValue { get; private set; }
        public double? DoubleValue { get; private set; }

        public Setting(string name, string settingString, string description = null)
        {
            Name = Name;
            Description = description;
            Type = ESettingType.String;
            StringValue = settingString;
        }

        public Setting(string name, int settingInt, string description = null)
        {
            Name = Name;
            Description = description;
            Type = ESettingType.Int;
            IntValue = settingInt;
            StringValue = null;
            DoubleValue = null;
        }

        public Setting(string name, double settingDouble, string description = null)
        {
            Name = Name;
            Description = description;
            Type = ESettingType.Double;
            DoubleValue = settingDouble;
            IntValue = null;
            StringValue = null;
        }

        public void ChangeTo<T>(T newValue)
        {
            Type = DetermineType<T>(newValue);

            switch (Type)
            {
                case ESettingType.String:
                    StringValue = (string) Convert.ChangeType(newValue, typeof(string));
                    IntValue = null;
                    DoubleValue = null;
                    break;

                case ESettingType.Int:
                    IntValue = (int) Convert.ChangeType(newValue, typeof(int));
                    StringValue = null;
                    DoubleValue = null;
                    break;

                case ESettingType.Double:
                    DoubleValue = (double) Convert.ChangeType(newValue, typeof(double));
                    StringValue = null;
                    IntValue = null;
                    break;

                default:
                    throw new ArgumentException($"Expected ESettingType, got value {Type} with type {typeof(Type).Name}");
            }
        }

        public ESettingType DetermineType<T>(T type)
        {
            string typeName = type.GetType().Name;

            foreach (ESettingType settingType in (ESettingType[]) Enum.GetValues(typeof(ESettingType)))
            {
                if (typeName.ToLower() == settingType.ToString().ToLower()) return settingType;
            }

            // No match found?
            throw new ArgumentException($"Expected ESettingType, got value {type} with type {typeName}");
        }
    }
}
