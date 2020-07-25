using EthosClient.API;
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

        public bool AntiKick = true;

        public bool VideoPlayerSafety = true;

        public bool AntiBlock = false;

        public bool PortalSafety = true;

        public bool AntiWorldTriggers = true;

        public bool UseRichPresence = false;

        public List<EthosVRButton> Buttons = new List<EthosVRButton>()
        {
            new EthosVRButton("MainMenu", "ShortcutMenu", "Ethos\nClient", "A client for vrchat's il2cpp system, hopefully just an updated version of my old publicly sold client, with more features and fixed bugs of course.", 5, 2, new EthosColorScheme(Color.red, Color.white, Color.red, Color.cyan), true),
            new EthosVRButton("Developer", "UIElementsMenu", "Developer\nOnly", "Just some experimental features I guess", 3, 0, new EthosColorScheme(Color.red, Color.white, Color.red, Color.cyan), false),
            new EthosVRButton("ExtendedFavorites", null, "Extended\nFavorites", "Open up the extended favorites menu and add more avatars than the default limit of 16", 4, 1, new EthosColorScheme(Color.red, Color.white, Color.red, Color.cyan), true),
            new EthosVRButton("Fun", null, "Fun", "A menu full of fun stuff!", 2, 1, new EthosColorScheme(Color.red, Color.white, Color.red, Color.cyan), true),
            new EthosVRButton("Protections", null, "Protections", "A menu full of protection options against moderation, and other safety related features.", 3, 1, new EthosColorScheme(Color.red, Color.white, Color.red, Color.cyan), true),
            new EthosVRButton("PlayerOptions", "UserInteractMenu", "Player\nOptions", "Open this menu and control what you want of other players.", 1, 2, new EthosColorScheme(Color.red, Color.white, Color.red, Color.cyan), true),
            new EthosVRButton("Utils", null, "Utils", "Extended utilities you can use to manage the game better", 1, 1, new EthosColorScheme(Color.red, Color.white, Color.red, Color.cyan), true)
        };
    }
}
