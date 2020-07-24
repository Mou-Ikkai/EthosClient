using EthosClient.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EthosClient.Settings
{
    public static class Configuration
    {
        private static Config _Config { get; set; }

        public static IntPtr HWIDP = IntPtr.Zero;

        public static void SaveConfiguration()
        {
            File.WriteAllText("EthosClient\\Configuration.json", JsonConvert.SerializeObject(_Config, Formatting.Indented));
        }

        public static void CheckExistence()
        {
            Directory.CreateDirectory("EthosClient");
            if (!File.Exists($"EthosClient\\Configuration.json"))
                File.WriteAllText("EthosClient\\Configuration.json", JsonConvert.SerializeObject(new Config(), Formatting.Indented));
            LoadConfiguration();
        }

        public static void LoadConfiguration() { _Config = JsonConvert.DeserializeObject<Config>(File.ReadAllText("EthosClient\\Configuration.json")); }
        
        public static Config GetConfig() { return _Config; }
    }
}
