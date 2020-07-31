using EthosClient.EthosInput;
using EthosClient.Menu;
using Newtonsoft.Json;
using System.IO;
using UnityEngine;

namespace EthosClient.Settings
{
    public static class Configuration
    {
        public const string ConfigLocation = "EthosClient\\Config.json";

        private static Config _Config { get; set; }

        public static void SaveConfiguration() =>
            File.WriteAllText(ConfigLocation, JsonConvert.SerializeObject(_Config, Formatting.Indented));

        public static void CheckExistence()
        {
            Directory.CreateDirectory("EthosClient");
            if (!File.Exists(ConfigLocation))
            {
                Config config = new Config();
                File.WriteAllText(ConfigLocation, JsonConvert.SerializeObject(config, Formatting.Indented));
            }
            else 
                LoadConfiguration();

            #region Checking for button and keybind existence
            if (!_Config.Buttons.ContainsKey("MainMenu"))
                _Config.Buttons.Add("MainMenu", new EthosVRButton("MainMenu", "ShortcutMenu", "Ethos\nClient", "A client for vrchat's il2cpp system, hopefully just an updated version of my old publicly sold client, with more features and fixed bugs of course.", 5, 2, new EthosColorScheme(Color.red, Color.white, Color.red, Color.white), true));
            
            if (!_Config.Buttons.ContainsKey("Developer"))
                _Config.Buttons.Add("Developer", new EthosVRButton("Developer", "UIElementsMenu", "Developer\nOnly", "Just some experimental features I guess", 3, 0, new EthosColorScheme(Color.red, Color.white, Color.red, Color.white), false));
            
            if (!_Config.Buttons.ContainsKey("ExtendedFavorites"))
                _Config.Buttons.Add("ExtendedFavorites", new EthosVRButton("ExtendedFavorites", null, "Extended\nFavorites", "Open up the extended favorites menu and add more avatars than the default limit of 16.", 4, 1, new EthosColorScheme(Color.red, Color.white, Color.red, Color.white), true));
            
            if (!_Config.Buttons.ContainsKey("Fun"))
                _Config.Buttons.Add("Fun", new EthosVRButton("Fun", null, "Fun", "A menu full of fun stuff!", 2, 1, new EthosColorScheme(Color.red, Color.white, Color.red, Color.white), true));
            
            if (!_Config.Buttons.ContainsKey("Protections"))
                _Config.Buttons.Add("Protections", new EthosVRButton("Protections", null, "Protections", "A menu full of protection options against moderation, and other safety related features.", 3, 1, new EthosColorScheme(Color.red, Color.white, Color.red, Color.white), true));

            if (!_Config.Buttons.ContainsKey("PlayerOptions"))
                _Config.Buttons.Add("PlayerOptions", new EthosVRButton("PlayerOptions", "UserInteractMenu", "Player\nOptions", "Open this menu and control what you want of other players.", 1, 2, new EthosColorScheme(Color.red, Color.white, Color.red, Color.white), true));

            if (!_Config.Buttons.ContainsKey("Utils"))
                _Config.Buttons.Add("Utils", new EthosVRButton("Utils", null, "Utils", "Extended utilities you can use to manage the game better.", 1, 1, new EthosColorScheme(Color.red, Color.white, Color.red, Color.white), true));

            if (!_Config.Buttons.ContainsKey("Settings"))
                _Config.Buttons.Add("Settings", new EthosVRButton("Settings", null, "Settings", "Configure the client's settings and make it more comfortable for yourself.", 4, 2, new EthosColorScheme(Color.red, Color.white, Color.red, Color.white), true));

            if (!_Config.Buttons.ContainsKey("Keybinds"))
                _Config.Buttons.Add("Keybinds", new EthosVRButton("Keybinds", null, "Keybinds", "Allows you to easily configure client keybinds.", 2, 2, new EthosColorScheme(Color.red, Color.white, Color.red, Color.white), true));

            if (!_Config.Buttons.ContainsKey("VRUtils"))
                _Config.Buttons.Add("VRUtils", new EthosVRButton("VRUtils", null, "VR\nUtils", "Allows you to do stuff that would seem harder in VR, but allows you to execute tasks quick and fast.", 3, 2, new EthosColorScheme(Color.red, Color.white, Color.red, Color.white), true));

            if (!_Config.Keybinds.ContainsKey("Flight"))
                _Config.Keybinds.Add("Flight", new EthosKeybind(EthosFeature.Flight, KeyCode.LeftAlt, KeyCode.F, true));

            if (!_Config.Keybinds.ContainsKey("Autism"))
                _Config.Keybinds.Add("Autism", new EthosKeybind(EthosFeature.Autism, KeyCode.LeftAlt, KeyCode.A, true));

            if (!_Config.Keybinds.ContainsKey("SpinBot"))
                _Config.Keybinds.Add("SpinBot", new EthosKeybind(EthosFeature.SpinBot, KeyCode.LeftAlt, KeyCode.S, true));

            if (!_Config.Keybinds.ContainsKey("ESP"))
                _Config.Keybinds.Add("ESP", new EthosKeybind(EthosFeature.ESP, KeyCode.LeftAlt, KeyCode.E, true));

            if (!_Config.Keybinds.ContainsKey("WorldTriggers"))
                _Config.Keybinds.Add("WorldTriggers", new EthosKeybind(EthosFeature.WorldTriggers, KeyCode.LeftAlt, KeyCode.W, true));

            if (!_Config.Keybinds.ContainsKey("ToggleAllTriggers"))
                _Config.Keybinds.Add("ToggleAllTriggers", new EthosKeybind(EthosFeature.ToggleAllTriggers, KeyCode.LeftAlt, KeyCode.T, true));

            #endregion
        }

        public static void LoadConfiguration()
        {
            _Config = JsonConvert.DeserializeObject<Config>(File.ReadAllText(ConfigLocation));
            if (_Config == null)
            {
                _Config = new Config();
            }
        }
        public static Config GetConfig() =>
            _Config;
    }
}
