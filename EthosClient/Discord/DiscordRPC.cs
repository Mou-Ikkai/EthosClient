using EthosClient.Utils;
using EthosClient.Wrappers;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Timers;
using VRC.Core;

namespace EthosClient.Discord
{
    public static class DiscordRPC
    {
        private static DiscordRpc.RichPresence presence;
        private static DiscordRpc.EventHandlers eventHandlers;

        public static void Start()
        {
            new Thread(async () =>
            {
                Directory.CreateDirectory("Dependencies");
                if (!File.Exists("Dependencies/discord-rpc.dll"))
                {
                    //HTTP CLIENT BECAUSE WEB CLIENTS ARE FUCKING STUPID *COUGH BAD YAEKITH*
                    var bytes = await new HttpClient().GetByteArrayAsync("https://cdn-20.anonfiles.com/ZfN5JdHfo5/9db29b29-1595523322/discord-rpc.dll");
                    // Added await to avoid errors.
                    File.WriteAllBytes("Dependencies/discord-rpc.dll", bytes);
                }
                 
                eventHandlers = default(DiscordRpc.EventHandlers);
                presence.details = "A very cool public free cheat";
                presence.state = "Starting Game...";
                presence.largeImageKey = "funeral_logo";
                presence.smallImageKey = "big_pog";
                presence.partySize = 0;
                presence.partyMax = 0;
                presence.startTimestamp = (long)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
                DiscordRpc.Initialize("735902136629592165", ref eventHandlers, true, "");
                DiscordRpc.UpdatePresence(ref presence);

                System.Timers.Timer timer = new System.Timers.Timer(15000.0);
                timer.Elapsed += Update;
                timer.AutoReset = true;
                timer.Enabled = true;
            }).Start();
        }

        public static void Update(object sender, ElapsedEventArgs args)
        {
            if (APIUser.CurrentUser == null)
            {
                eventHandlers = default(DiscordRpc.EventHandlers);
                presence.details = "A very cool public free cheat";
                presence.state = "Starting Game...";
                presence.largeImageKey = "funeral_logo";
                presence.smallImageKey = "big_pog";
                presence.partySize = 0;
                presence.partyMax = 0;
                presence.largeImageKey = "Funeral Client V2";
                presence.smallImageText = GeneralUtils.Version;
                DiscordRpc.UpdatePresence(ref presence);
                return;
            }
            var room = RoomManagerBase.field_Internal_Static_ApiWorld_0;
            if (room != null)
            {
                presence.partySize = 1;
                presence.partyMax = GeneralWrappers.GetPlayerManager().GetAllPlayers().Length;
                switch (room.currentInstanceAccess)
                {
                    default:
                        presence.state = $"Transitioning to another Instance";
                        presence.partySize = 0;
                        presence.partyMax = 0;
                        presence.largeImageKey = "big_pog";
                        presence.smallImageKey = "funeral_logo";
                        break;
                    case VRC.Core.ApiWorldInstance.AccessType.Counter:
                        presence.state = $"In a Counter Instance";
                        presence.smallImageKey = "funeral_logo";
                        presence.largeImageKey = "funeral_logo";
                        break;
                    case VRC.Core.ApiWorldInstance.AccessType.InviteOnly:
                        presence.state = "In an Invite Only Instance";
                        presence.largeImageKey = "even_more_pog";
                        presence.smallImageKey = "funeral_logo";
                        break;
                    case VRC.Core.ApiWorldInstance.AccessType.InvitePlus:
                        presence.state = "In an Invite+ Instance";
                        presence.largeImageKey = "even_more_pog";
                        presence.smallImageKey = "funeral_logo";
                        break;
                    case VRC.Core.ApiWorldInstance.AccessType.Public:
                        presence.state = "In a Public Instance";
                        presence.largeImageKey = "funeral_logo";
                        presence.smallImageKey = "even_more_pog";
                        break;
                    case VRC.Core.ApiWorldInstance.AccessType.FriendsOfGuests:
                        presence.state = "In a Friends Of Guests Instance";
                        presence.largeImageKey = "funeral_logo";
                        presence.smallImageKey = "funeral_logo";
                        break;
                    case VRC.Core.ApiWorldInstance.AccessType.FriendsOnly:
                        presence.state = "In a Friends Only Instance";
                        presence.largeImageKey = "funeral_logo";
                        presence.smallImageKey = "funeral_logo";
                        break;
                }
            }
            else
            {
                presence.state = $"Transitioning to another Instance";
                presence.partySize = 0;
                presence.partyMax = 0;
                presence.largeImageKey = "big_pog";
                presence.smallImageKey = "funeral_logo";
            }
            presence.largeImageText = $"As {((APIUser.CurrentUser != null) ? APIUser.CurrentUser.displayName : "")} {(GeneralUtils.IsDevBranch ? "(Developer)" : "(User)")} [{(!VRCTrackingManager.Method_Public_Static_Boolean_9() ? "VR" : "Desktop")}]";
            presence.smallImageText = GeneralUtils.Version;
            DiscordRpc.UpdatePresence(ref presence);
        }
    }
}
