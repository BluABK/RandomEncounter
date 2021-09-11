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

    public class SettingsHandler : ISettingsHandler
    {
        private UnicodeEncoding uniEncoding = new UnicodeEncoding();
        public readonly string AppDirName = "RandomEncounter";
        private string appDataDir { get; set; }
        public string Filename { get; set; }
        public string DefaultPath { get; set; }
        public string FilePath { get; set; }
        public Settings Settings { get; set; }
        public JsonSerializerOptions JsonOptions { get; set; }
        public SettingsHandler(string filename = "settings.json", bool saveDefaultFile = true, JsonSerializerOptions jsonOptions = null)
        {
            Filename = filename;
            appDataDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            DefaultPath = Path.Combine(appDataDir, AppDirName);
            FilePath = Path.Combine(appDataDir, AppDirName, Filename);

            JsonOptions = jsonOptions ?? new JsonSerializerOptions() { WriteIndented = true };

            Settings = new();

            if (File.Exists(FilePath))
            {
                // Handle existing settings file.
                GenerateDefaultSettings();
                Load();
            } else
            {
                // Create settings file, if none exist (using defaults).
                CreateSettingsFile(saveDefaultFile);
            }
        }

        public void GenerateDefaultSettings()
        {
            // Add settings.
            Settings.Add("CreatureDefinitionsFilePath", Path.Combine(DefaultPath, "Creatures.json"));
            Settings.Add("MoveDefinitionsFilePath", Path.Combine(DefaultPath, "Moves.json"));
            Settings.Add("ItemDefinitionsFilePath", Path.Combine(DefaultPath, "Items.json"));
            Settings.Add("TypeDefinitionsFilePath", Path.Combine(DefaultPath, "Types.json"));
        }

        public void CreateSettingsFile(bool save = true)
        {
            GenerateDefaultSettings();

            if (save) Save();
        }

        public void Save()
        {
            // Create App dir (if not exist).
            Directory.CreateDirectory(DefaultPath);

            // Write file
            using ( FileStream fs = new FileStream(FilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite) )
            {
                // Write data to file
                foreach (Setting setting in Settings.GetList())
                {
                    // Determine setting value type, and create JSON string accordingly.
                    string jsonString = string.Empty;
                    switch (setting.Type)
                    {
                        case ESettingType.String:
                            jsonString = JsonSerializer.Serialize( new JsonSetting<string>(setting.Name, setting.StringValue, setting.Description), JsonOptions );
                            break;

                        case ESettingType.Int:
                            jsonString = JsonSerializer.Serialize( new JsonSetting<int?>(setting.Name, setting.IntValue, setting.Description), JsonOptions );
                            break;

                        case ESettingType.Double:
                            jsonString = JsonSerializer.Serialize( new JsonSetting<double?>(setting.Name, setting.DoubleValue, setting.Description), JsonOptions );
                            break;

                        default:
                            throw new ArgumentException($"Expected ESettingType, but got {setting.Type}");
                    }
                    
                    // Write JSON string to file.
                    fs.Write(uniEncoding.GetBytes(jsonString), 0, uniEncoding.GetByteCount(jsonString));
                }
            }
        }

        public void Load()
        {
            string json = string.Empty;

            // Read file
            using (StreamReader r = new StreamReader(FilePath))
            {
                json = r.ReadToEnd();
            }

            //JsonSerializer.Deserialize<Item>(json);

        }

        public void Reload()
        {
            throw new NotImplementedException();
        }

        public void Get(string settingName)
        {
            Settings.Get(settingName);
        }

        public void Modify(string name, string value)
        {
            Settings.Modify(name, value);
        }

        public void Modify(string name, int value)
        {
            Settings.Modify(name, value);
        }

        public void Modify(string name, double value)
        {
            Settings.Modify(name, value);
        }
    }
}
