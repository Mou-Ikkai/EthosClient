using FuneralClientV2.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuneralClientV2.Settings
{
    public static class Configuration
    {
        private static Config _Config { get; set; }

        public static IntPtr HWIDP = IntPtr.Zero;

        public static void SaveConfiguration()
        {
            File.WriteAllText("FuneralClientV2\\Configuration.json", JsonConvert.SerializeObject(_Config, Formatting.Indented));
        }

        public static void CheckExistence()
        {
            Directory.CreateDirectory("FuneralClientV2");
            if (!File.Exists($"FuneralClientV2\\Configuration.json"))
                File.WriteAllText("FuneralClientV2\\Configuration.json", JsonConvert.SerializeObject(new Config(), Formatting.Indented));
            LoadConfiguration();
        }

        public static void LoadConfiguration() { _Config = JsonConvert.DeserializeObject<Config>(File.ReadAllText("FuneralClientV2\\Configuration.json")); }
        
        public static Config GetConfig() { return _Config; }
    }
}
