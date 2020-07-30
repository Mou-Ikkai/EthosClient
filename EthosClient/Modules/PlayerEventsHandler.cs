using EthosClient.EthosInput;
using EthosClient.Settings;
using EthosClient.Utils;
using EthosClient.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using VRC;
using VRC.Core;
using VRC.SDKBase;

namespace EthosClient.Modules
{
    public class PlayerEventsHandler : VRCMod
    {
        public override string Name => "Player Events Handler";

        public override string Description => "Handlers for when players join, block others, etc.";

        public override string Author => "Yaekith#1337";

        public override void OnStart() { }

        public override void OnUpdate() { }

        public override void OnPlayerJoin(Player player)
        {
            if (GeneralUtils.Authorities.TryGetValue(player.GetAPIUser().id, out string what))
            {
                //im gonna use the what for later ok
                player.GetVRCPlayerApi().displayName = "[Ethos Team] " + player.GetVRCPlayerApi().displayName;
                player.GetVRCPlayerApi().SetNamePlateColor(Color.cyan);
                ConsoleUtil.Info($"An Ethos Admin+ || {player.GetAPIUser().displayName} has joined.");
            }

            if (Configuration.GetConfig().LogModerations)
                GeneralUtils.InformHudText(Color.green, $"{player.GetAPIUser().displayName} has joined.");

            if (GeneralUtils.ESP)
            {
                GameObject[] array = GameObject.FindGameObjectsWithTag("Player");
                for (int i = 0; i < array.Length; i++)
                {
                    if (array[i].transform.Find("SelectRegion"))
                    {
                        array[i].transform.Find("SelectRegion").GetComponent<Renderer>().material.SetColor("_Color", Color.red);
                        array[i].transform.Find("SelectRegion").GetComponent<Renderer>().sharedMaterial.SetColor("_Color", Color.magenta);
                        GeneralWrappers.GetHighlightsFX().EnableOutline(array[i].transform.Find("SelectRegion").GetComponent<Renderer>(), GeneralUtils.ESP);
                    }
                }
            }
        }

        public override void OnPlayerLeft(Player player)
        {
            if (Configuration.GetConfig().LogModerations) 
                GeneralUtils.InformHudText(Color.green, $"{player.GetAPIUser().displayName} has left.");
        }

        public override void OnPlayerKicked(Player moderator, Player kicked)
        {
            if (Configuration.GetConfig().LogModerations)
            {
                HudPrint(moderator, kicked, (sender, target, isyou) =>
                {
                    if (isyou)
                        GeneralUtils.InformHudText(Color.red, $"You were attempt kicked by {sender.GetAPIUser().displayName}");
                    else
                        GeneralUtils.InformHudText(Color.red, $"{target.GetAPIUser().displayName} has been kicked by {sender.GetAPIUser().displayName}");
                });
            }
        }

        public override void OnPlayerBanned(Player moderator, Player banned)
        {
            if (Configuration.GetConfig().LogModerations)
            {
                HudPrint(moderator, banned, (sender, target, isyou) =>
                {
                    if (isyou)
                        GeneralUtils.InformHudText(Color.red, $"You were attempt public banned by {sender.GetAPIUser().displayName}");
                    else
                        GeneralUtils.InformHudText(Color.red, $"{target.GetAPIUser().displayName} has been public banned by {sender.GetAPIUser().displayName}");
                });
            }
        }

        public override void OnPlayerBlocked(Player blocker, Player blocked, bool state)
        {
            if (Configuration.GetConfig().LogModerations)
            {
                HudPrint(blocker, blocked, (sender, target, isyou) =>
                {
                    if (isyou)
                        GeneralUtils.InformHudText(Color.red, $"You were {(state ? "blocked" : "unblocked")} by {sender.GetAPIUser().displayName}");
                    else
                        GeneralUtils.InformHudText(Color.red, $"{target.GetAPIUser().displayName} has been {(state ? "blocked" : "unblocked")} by {sender.GetAPIUser().displayName}");
                });
            }
        }

        public override void OnPlayerPublicBanned(Player moderator, Player banned)
        {
            if (Configuration.GetConfig().LogModerations)
            {
                HudPrint(moderator, banned, (sender, target, isyou) =>
                {
                    if (isyou)
                        GeneralUtils.InformHudText(Color.red, $"You were attempt public banned by {sender.GetAPIUser().displayName}");
                    else
                        GeneralUtils.InformHudText(Color.red, $"{target.GetAPIUser().displayName} has been public banned by {sender.GetAPIUser().displayName}");
                });
            }
        }

        public override void OnPlayerFriended(Player friender, Player friended)
        {
            if (Configuration.GetConfig().LogModerations)
            {
                HudPrint(friender, friended, (sender, target, isyou) =>
                {
                    if (isyou)
                        GeneralUtils.InformHudText(Color.red, $"You were friended/unfriended by {sender.GetAPIUser().displayName}");
                    else
                        GeneralUtils.InformHudText(Color.red, $"{target.GetAPIUser().displayName} has been friended/unfriended by {sender.GetAPIUser().displayName}");
                });
            }
        }

        public override void OnPlayerLoggedOut(Player moderator, Player target)
        {
            if (Configuration.GetConfig().LogModerations)
            {
                HudPrint(moderator, target, (sender, target2, isyou) =>
                {
                    if (isyou)
                        GeneralUtils.InformHudText(Color.red, $"You were attempt logged out by {sender.GetAPIUser().displayName}");
                    else
                        GeneralUtils.InformHudText(Color.red, $"{target2.GetAPIUser().displayName} has been logged by {sender.GetAPIUser().displayName}");
                });
            }
        }

        public override void OnPlayerMicOff(Player moderator, Player target)
        {
            if (Configuration.GetConfig().LogModerations)
            {
                HudPrint(moderator, target, (sender, target2, isyou) =>
                {
                    if (isyou)
                        GeneralUtils.InformHudText(Color.red, $"You had your microphone attempt forced off by {sender.GetAPIUser().displayName}");
                    else
                        GeneralUtils.InformHudText(Color.red, $"{target2.GetAPIUser().displayName} has had their microphone forced off by {sender.GetAPIUser().displayName}");
                });
            }
        }

        public override void OnPlayerMuted(Player muter, Player muted, bool state)
        {
            if (Configuration.GetConfig().LogModerations)
            {
                HudPrint(muter, muted, (sender, target, isyou) =>
                {
                    if (isyou)
                        GeneralUtils.InformHudText(Color.red, $"You were {(state ? "muted" : "unmuted")} by {sender.GetAPIUser().displayName}");
                    else
                        GeneralUtils.InformHudText(Color.red, $"{target.GetAPIUser().displayName} were {(state ? "muted" : "unmuted")} by {sender.GetAPIUser().displayName}");
                });
            }
        }

        public override void OnPlayerShown(Player user, Player who, bool state)
        {
            if (Configuration.GetConfig().LogModerations)
            {
                HudPrint(user, who, (sender, target, isyou) =>
                {
                    if (isyou)
                        GeneralUtils.InformHudText(Color.red, $"You were {(state ? "shown" : "hidden")} by {sender.GetAPIUser().displayName}");
                    else
                        GeneralUtils.InformHudText(Color.red, $"{target.GetAPIUser().displayName} has been {(state ? "shown" : "hidden")} by {sender.GetAPIUser().displayName}");
                });
            }
        }

        public override void OnPlayerWarned(Player moderator, Player warned)
        {
            if (Configuration.GetConfig().LogModerations)
            {
                HudPrint(moderator, warned, (sender, target, isyou) =>
                {
                    if (isyou)
                        GeneralUtils.InformHudText(Color.red, $"You were warned by {sender.GetAPIUser().displayName}");
                    else
                        GeneralUtils.InformHudText(Color.red, $"{target.GetAPIUser().displayName} has been warned by {sender.GetAPIUser().displayName}");
                });
            }
        }

        public static void HudPrint(Player other, Player ply, Action<Player, Player, bool> dothis)
        {
            if (ply == null)
                return;
            if (APIUser.CurrentUser == null)
                return;
            if (ply.GetAPIUser() == null)
                return;
            var target = other;
            if (target == null)
                return;
            if (target.GetAPIUser() == null)
                return;
            dothis?.Invoke(ply, target, (target.GetAPIUser().id == APIUser.CurrentUser.id));
        }
    }
}
