using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;

namespace RandomEncounterConsole.Handlers
{
    public class Setting<T>
    {
        public string Name { get; set; }
        public T Value { get; set; }
        public string Description { get; set; }
    }

    public class SettingsHandler : ISettingsHandler
    {
        private UnicodeEncoding uniEncoding = new UnicodeEncoding();
        public readonly string AppDirName = "RandomEncounter";
        private string appDataDir { get; set; }
        public string Filename { get; set; }
        public string DefaultFilePath { get; set; }
        public string FilePath { get; set; }
        public List<string> JsonSettings { get; set; }
        public SettingsHandler(string filename = "settings.json", bool saveDefaultFile = true)
        {
            Filename = filename;
            appDataDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            DefaultFilePath = Path.Combine(appDataDir, AppDirName);
            FilePath = Path.Combine(appDataDir, AppDirName, Filename);

            System.Console.WriteLine(DefaultFilePath);

            JsonSettings = new();

            // Default settings:
            GenerateDefaultFile(saveDefaultFile);
        }

        public void AddSetting<T>(string name, T value, string description)
        {
            Setting<T> setting = new Setting<T>() { Name = name, Value = value, Description = description };

            JsonSettings.Add(JsonSerializer.Serialize(setting));
        }

        public void GenerateDefaultFile(bool save = true)
        {
            // Add settings.
            AddSetting("CreatureDefinitionsFilePath", Path.Combine(DefaultFilePath, "Creatures.json"), "JSON file containing creature definitions.");
            AddSetting("MoveDefinitionsFilePath", Path.Combine(DefaultFilePath, "Moves.json"), "JSON file containing move definitions.");
            AddSetting("ItemDefinitionsFilePath", Path.Combine(DefaultFilePath, "Items.json"), "JSON file containing item definitions.");
            AddSetting("TypeDefinitionsFilePath", Path.Combine(DefaultFilePath, "Types.json"), "JSON file containing type definitions.");

            // Save.
            if (save) Save();
        }

        public void Save()
        {
            // Create App dir (if not exist).
            Directory.CreateDirectory(DefaultFilePath);

            // Write file
            using ( FileStream fs = new FileStream(FilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite) )
            {
                // Write data to file
                foreach (string setting in JsonSettings)
                {
                    fs.Write(uniEncoding.GetBytes(setting), 0, uniEncoding.GetByteCount(setting));
                }
            }
        }

        public void Load()
        {
            throw new NotImplementedException();
        }

        public void Reload()
        {
            throw new NotImplementedException();
        }

        public void GetOption()
        {
            throw new NotImplementedException();
        }
        public void SetOption()
        {
            throw new NotImplementedException();
        }
    }
}
