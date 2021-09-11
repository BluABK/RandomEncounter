namespace RandomEncounterConsole.Handlers
{
    public interface ISettingsHandler
    {
        public string Filename { get; set; }

        public void GenerateDefaultFile(bool save = true);

        public void Save();

        public void Load();

        public void Reload();

        public void GetOption();
        public void SetOption();
    }
}