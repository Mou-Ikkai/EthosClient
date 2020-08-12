using EthosClient.EthosInput;
using EthosClient.Menu;
using EthosClient.Utils;
using Newtonsoft.Json;
using System.ComponentModel.Design;
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
                var config = new Config();
                config.Buttons.Add(new EthosVRButton("MainMenu", "CameraMenu", "Ethos\nClient", "A client for vrchat's il2cpp system, hopefully just an updated version of my old publicly sold client, with more features and fixed bugs of course.", 4, 0, new EthosColorScheme(Color.red, Color.white, Color.red, Color.white), true));
                config.Buttons.Add(new EthosVRButton("Developer", "UIElementsMenu", "Developer\nOnly", "Just some experimental features I guess", 3, 0, new EthosColorScheme(Color.red, Color.white, Color.red, Color.white), false));
                config.Buttons.Add(new EthosVRButton("ExtendedFavorites", null, "Extended\nFavorites", "Open up the extended favorites menu and add more avatars than the default limit of 16.", 4, 1, new EthosColorScheme(Color.red, Color.white, Color.red, Color.white), true));
                config.Buttons.Add(new EthosVRButton("Fun", null, "Fun", "A menu full of fun stuff!", 2, 1, new EthosColorScheme(Color.red, Color.white, Color.red, Color.white), true));
                config.Buttons.Add(new EthosVRButton("Protections", null, "Protections", "A menu full of protection options against moderation, and other safety related features.", 3, 1, new EthosColorScheme(Color.red, Color.white, Color.red, Color.white), true));
                config.Buttons.Add(new EthosVRButton("PlayerOptions", "UserInteractMenu", "Player\nOptions", "Open this menu and control what you want of other players.", 1, 2, new EthosColorScheme(Color.red, Color.white, Color.red, Color.white), true));
                config.Buttons.Add(new EthosVRButton("Utils", null, "Utils", "Extended utilities you can use to manage the game better.", 1, 1, new EthosColorScheme(Color.red, Color.white, Color.red, Color.white), true));
                config.Buttons.Add(new EthosVRButton("Settings", null, "Settings", "Configure the client's settings and make it more comfortable for yourself.", 4, 2, new EthosColorScheme(Color.red, Color.white, Color.red, Color.white), true));
                config.Buttons.Add(new EthosVRButton("Keybinds", null, "Keybinds", "Allows you to easily configure client keybinds.", 2, 2, new EthosColorScheme(Color.red, Color.white, Color.red, Color.white), true));
                config.Buttons.Add(new EthosVRButton("VRUtils", null, "VR\nUtils", "Allows you to do stuff that would seem harder in VR, but allows you to execute tasks quick and fast.", 3, 2, new EthosColorScheme(Color.red, Color.white, Color.red, Color.white), true));
                config.Keybinds.Add(new EthosKeybind(EthosFeature.Flight, KeyCode.LeftAlt, KeyCode.F, true));
                config.Keybinds.Add(new EthosKeybind(EthosFeature.Autism, KeyCode.LeftAlt, KeyCode.A, true));
                config.Keybinds.Add(new EthosKeybind(EthosFeature.SpinBot, KeyCode.LeftAlt, KeyCode.S, true));
                config.Keybinds.Add(new EthosKeybind(EthosFeature.ESP, KeyCode.LeftAlt, KeyCode.E, true));
                config.Keybinds.Add(new EthosKeybind(EthosFeature.WorldTriggers, KeyCode.LeftAlt, KeyCode.W, true));
                config.Keybinds.Add(new EthosKeybind(EthosFeature.ToggleAllTriggers, KeyCode.LeftAlt, KeyCode.T, true));
                File.WriteAllText(ConfigLocation, JsonConvert.SerializeObject(config, Formatting.Indented));
            }
            else 
                LoadConfiguration();
        }

        public static void LoadConfiguration()
        {
            _Config = JsonConvert.DeserializeObject<Config>(File.ReadAllText(ConfigLocation));
            if (_Config.ClientVersion != GeneralUtils.Version)
                ConsoleUtil.Info("YOU ARE USING AN OUTDATED VERSION OF ETHOS. THINGS MAY BE UNSTABLE AND CRASH, DEPENDING ON YOUR VRCHAT BUILD. PLEASE UPGRADE ASAP IF POSSIBLE FROM: https://github.com/Yaekith/EthosClient/releases");
                CheckExistence();
        }

        public static Config GetConfig() => _Config;
    }
}
