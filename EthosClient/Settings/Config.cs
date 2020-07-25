using EthosClient.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public int MainMenuButtonX = 5;

        public int MainMenuButtonY = 2;
    }
}
