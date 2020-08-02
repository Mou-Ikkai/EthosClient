using EthosClient.API;
using EthosClient.EthosInput;
using EthosClient.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace EthosClient.Settings
{
    public class Config
    {
        public List<FavoritedAvatar> ExtendedFavoritedAvatars = new List<FavoritedAvatar>();

        public bool CleanConsole = true;

        public bool Optimization = true;

        public bool LogModerations = false;

        public bool AntiPublicBan = true;

        public bool AntiKick = false;

        public bool VideoPlayerSafety = true;

        public bool AntiBlock = false;

        public bool PortalSafety = true;

        public bool AntiWorldTriggers = false;

        public bool UseRichPresence = true;

        public bool MenuRGB = false;

        public bool SideUI = false;

        public bool DefaultLogToConsole = true;

        public Dictionary<string, EthosVRButton> Buttons = new Dictionary<string, EthosVRButton>();

        public Dictionary<string, EthosKeybind> Keybinds = new Dictionary<string, EthosKeybind>();
    }
}
