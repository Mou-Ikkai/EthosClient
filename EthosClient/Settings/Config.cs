using EthosClient.API;
using EthosClient.EthosInput;
using EthosClient.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace EthosClient.Settings
{
    public class Config
    {
        public bool 
            CleanConsole = true, 
            Optimization = false, 
            LogModerations = false, 
            AntiPublicBan = true, 
            AntiKick = true, 
            VideoPlayerSafety = true, 
            AntiBlock = true, 
            PortalSafety = true, 
            AntiWorldTriggers = true, 
            UseRichPresence = true, 
            MenuRGB = false, 
            DefaultLogToConsole = true;

        public List<FavoritedAvatar> ExtendedFavoritedAvatars = new List<FavoritedAvatar>();

        public Dictionary<string, EthosVRButton> Buttons = new Dictionary<string, EthosVRButton>();

        public Dictionary<string, EthosKeybind> Keybinds = new Dictionary<string, EthosKeybind>();
    }
}
