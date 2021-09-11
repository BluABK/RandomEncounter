namespace RandomEncounterConsole.Handlers
{
    public interface ISetting
    {
        public string Name { get; }
        public string Description { get; }
        public ESettingType Type { get; }
        public string StringValue { get; }
        public int? IntValue { get; }
        public double? DoubleValue { get; }
        public void ChangeTo<T>(T newValue);
        public ESettingType DetermineType<T>(T type);
    }
}