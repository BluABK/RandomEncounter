namespace RandomEncounterConsole.Handlers
{
    public interface ISettingsHandler
    {
        public string Filename { get; set; }

        public void CreateSettingsFile(bool save = true);

        public void Save();

        public void Load();

        public void Reload();

        public void Get(string settingName);
        public void Modify(string name, string value);
        public void Modify(string name, int value);
        public void Modify(string name, double value);
    }
}