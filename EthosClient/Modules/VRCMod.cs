using EthosClient.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VRC;
using WebSocketSharp;

namespace EthosClient.Modules
{
    public class VRCMod
    {
        public virtual string Name => "Example Module";

        public virtual string Description => "Example Description";

        public virtual string Author => "Example Author";

        public VRCMod() => ConsoleUtil.Info($"VRC Mod {this.Name} has Loaded made by {Author}. {this.Description}");

        public virtual void OnStart()
        {

        }

        public virtual void OnAppQuit()
        {

        }

        public virtual void OnUiLoad()
        {

        }

        public virtual void OnUpdate()
        {

        }

        public virtual void OnPlayerJoin(Player player)
        {

        }

        public virtual void OnPlayerLeft(Player player)
        {

        }

        public virtual void OnPlayerBlocked(Player blocker, Player blocked, bool state)
        {

        }

        public virtual void OnPlayerKicked(Player moderator, Player kicked)
        {

        }

        public virtual void OnPlayerLoggedOut(Player moderator, Player target)
        {

        }

        public virtual void OnPlayerPublicBanned(Player moderator, Player banned)
        {

        }

        public virtual void OnPlayerBanned(Player moderator, Player banned)
        {

        }

        public virtual void OnPlayerFriended(Player friender, Player friended)
        {

        }

        public virtual void OnPlayerMuted(Player muter, Player muted, bool state)
        {

        }

        public virtual void OnPlayerShown(Player user, Player who, bool state)
        {

        }

        public virtual void OnPlayerWarned(Player moderator, Player warned)
        {

        }

        public virtual void OnPlayerMicOff(Player moderator, Player target)
        {

        }
    }
}
