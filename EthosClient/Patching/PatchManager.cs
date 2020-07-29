using EthosClient.Settings;
using EthosClient.Utils;
using EthosClient.Wrappers;
using Harmony;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnhollowerBaseLib;
using UnityEngine;
using VRC;
using VRC.Core;
using VRC.SDKBase;
using static VRC.SDKBase.VRC_EventHandler;

namespace EthosClient.Patching
{
    public static class PatchManager
    {
        private static HarmonyMethod GetLocalPatch(string name) { return new HarmonyMethod(typeof(PatchManager).GetMethod(name, BindingFlags.Static | BindingFlags.NonPublic)); }

        private static List<Patch> RetrievePatches()
        {
            var ConsoleWriteLine = AccessTools.Method(typeof(Il2CppSystem.Console), "WriteLine", new Type[] { typeof(string) });
            List<Patch> patches = new List<Patch>()
            {
                new Patch("Ethos_Extras", AccessTools.Method(typeof(VRC_EventHandler), "InternalTriggerEvent", null, null), GetLocalPatch("TriggerEvent"), null),
                new Patch("Ethos_Moderation", typeof(ModerationManager).GetMethod("KickUserRPC"), GetLocalPatch("AntiKick"), null),
                new Patch("Ethos_Moderation", typeof(ModerationManager).GetMethod("Method_Public_Boolean_String_String_String_1"), GetLocalPatch("CanEnterPublicWorldsPatch"), null),
                new Patch("Ethos_Moderation", typeof(ModerationManager).GetMethod("BlockStateChangeRPC"), GetLocalPatch("AntiBlock"), null),
                new Patch("Ethos_Moderation", typeof(ModerationManager).GetMethod("ForceLogoutRPC"), GetLocalPatch("AntiLogout"), null),
                new Patch("Ethos_Moderation", typeof(ModerationManager).GetMethod("BanPublicOnlyRPC"), GetLocalPatch("AntiPublicBan"), null),
                new Patch("Ethos_Moderation", typeof(ModerationManager).GetMethod("FriendStateChangeRPC"), GetLocalPatch("FriendPatch"), null),
                new Patch("Ethos_Moderation", typeof(ModerationManager).GetMethod("BanRPC"), GetLocalPatch("BanPatch"), null),
                new Patch("Ethos_Moderation", typeof(ModerationManager).GetMethod("MuteChangeRPC"), GetLocalPatch("MutePatch"), null),
                new Patch("Ethos_Moderation", typeof(ModerationManager).GetMethod("ShowUserAvatarChangedRPC"), GetLocalPatch("AvatarShownPatch"), null),
                new Patch("Ethos_Moderation", typeof(ModerationManager).GetMethod("WarnUserRPC"), GetLocalPatch("WarnPatch"), null),
                new Patch("Ethos_Moderation", typeof(ModerationManager).GetMethod("ModForceOffMicRPC"), GetLocalPatch("ModForceOffMicPatch"), null),
                new Patch("Ethos_Moderation", typeof(VRC_TriggerInternal).GetMethod("OnPlayerJoined"), GetLocalPatch("OnPlayerJoin"), null),
                new Patch("Ethos_Moderation", typeof(VRC_TriggerInternal).GetMethod("OnPlayerLeft"), GetLocalPatch("OnPlayerLeave"), null),
                new Patch("Ethos_Extras", typeof(UserInteractMenu).GetMethod("Update"), GetLocalPatch("CloneAvatarPrefix"), null),
                new Patch("Ethos_Extras", ConsoleWriteLine, GetLocalPatch("IL2CPPConsoleWriteLine"), null),
                new Patch("Ethos_Extras", typeof(ImageDownloader).GetMethod("DownloadImage"), GetLocalPatch("AntiIpLogImage"), null),
                new Patch("Ethos_Extras", typeof(VRCSDK2.VRC_SyncVideoPlayer).GetMethod("AddURL"), GetLocalPatch("AntiVideoPlayerHijacking"), null),
            };
            return patches;
        }

        public static void ApplyPatches()
        {
            var patches = RetrievePatches();
            foreach (var patch in patches) patch.ApplyPatch();
            ConsoleUtil.Info("All Patches have been applied successfully.");
        }

        #region Patches
        private static bool TriggerEvent(ref VrcEvent __0, ref VrcBroadcastType __1, ref int __2, ref float __3)
        {
            List<string> FilteredStrings = new List<string>()
            {
                "_InstantiateObject",
                "SetTimerRPC",
                "_DestroyObject",
                "_SendOnSpawn",
                "ConfigurePortal",
                "SceneEventHandlerAndInstantiator",
                "(Clone [100003] Portals/PortalInternalDynamic)"
            };
            if (GeneralUtils.IsDevBranch) Console.WriteLine(__0.ParameterObject.name);
            if (GeneralUtils.WorldTriggers) __1 = VrcBroadcastType.Always;
            if (Configuration.GetConfig().AntiWorldTriggers && !FilteredStrings.Contains(__0.ParameterObject.name.ToString())) return false;
            return true;
        }

        private static bool OnPlayerJoin(ref VRCPlayerApi __0)
        {
            if (Configuration.GetConfig().LogModerations) GeneralUtils.InformHudText(Color.green, $"{__0.displayName} has joined.");
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
            return true;
        }

        private static bool OnPlayerLeave(ref VRCPlayerApi __0)
        {
            if (Configuration.GetConfig().LogModerations) GeneralUtils.InformHudText(Color.green, $"{__0.displayName} has left.");
            return true;
        }

        private static bool AntiKick(ref string __0, ref string __1, ref string __2, ref string __3, ref Player __4)
        {
            if (Configuration.GetConfig().LogModerations)
                HudPrint(__0, __4, (sender, target, isyou) =>
                {
                    if (isyou)
                        GeneralUtils.InformHudText(Color.red, $"You were attempt kicked by {sender.GetAPIUser().displayName}");
                    else
                        GeneralUtils.InformHudText(Color.red, $"{target.GetAPIUser().displayName} has been kicked by {sender.GetAPIUser().displayName}");
                });
            return !Configuration.GetConfig().AntiKick;
        }

        private static bool AntiBlock(ref string __0, bool __1, ref Player __2)
        {
            if (Configuration.GetConfig().LogModerations)
                HudPrint(__0, __2, (sender, target, isyou) =>
                {
                    if (isyou)
                        GeneralUtils.InformHudText(Color.red, $"You were {(__1 ? "blocked" : "unblocked")} by {sender.GetAPIUser().displayName}");
                    else
                        GeneralUtils.InformHudText(Color.red, $"{target.GetAPIUser().displayName} has been {(__1 ? "blocked" : "unblocked")} by {sender.GetAPIUser().displayName}");
                });
            return !Configuration.GetConfig().AntiBlock;
        }

        private static void NonExistentPrefix() { }

        private static bool CloneAvatarPrefix(ref UserInteractMenu __instance)
        {
            bool result = true;
            if (GeneralUtils.ForceClone)
            {
                if (__instance.menuController.activeAvatar.releaseStatus != "private")
                {
                    bool flag2 = !__instance.menuController.activeUser.allowAvatarCopying;
                    if (flag2)
                    {
                        __instance.cloneAvatarButton.gameObject.SetActive(true);
                        __instance.cloneAvatarButton.interactable = true;
                        __instance.cloneAvatarButtonText.color = new Color(0.8117647f, 0f, 0f, 1f);
                        result = false;
                    }
                    else
                    {
                        __instance.cloneAvatarButton.gameObject.SetActive(true);
                        __instance.cloneAvatarButton.interactable = true;
                        __instance.cloneAvatarButtonText.color = new Color(0.470588237f, 0f, 0.8117647f, 1f);
                        result = false;
                    }
                }
            }
            return result;
        }

        private static bool IL2CPPConsoleWriteLine(string __0) { return !Configuration.GetConfig().CleanConsole; }

        private static bool AntiIpLogImage(string __0)
        {
            if (__0.StartsWith("https://api.vrchat.cloud/api/1/file/") || __0.StartsWith("https://api.vrchat.cloud/api/1/image/") || __0.StartsWith("https://d348imysud55la.cloudfront.net/thumbnails/") || __0.StartsWith("https://files.vrchat.cloud/thumbnails/")) return true;
            return !Configuration.GetConfig().PortalSafety;
        }

        private static bool AntiVideoPlayerHijacking(ref string __0)
        {
            if (Configuration.GetConfig().VideoPlayerSafety && GeneralUtils.SuitableVideoURL(__0)) __0 = "";
            return true;
        }

        private static bool CanEnterPublicWorldsPatch(ref bool __result, ref string __0, ref string __1, ref string __2)
        {
            __result = true;
            return true;
        }

        private static bool AntiLogout(ref string __0, ref Player __1)
        {
            if (Configuration.GetConfig().LogModerations)
                HudPrint(__0, __1, (sender, target, isyou) =>
                {

                    if (isyou)
                        GeneralUtils.InformHudText(Color.red, $"You were attempt logged out by {sender.GetAPIUser().displayName}");
                    else
                        GeneralUtils.InformHudText(Color.red, $"{target.GetAPIUser().displayName} has been logged out by {sender.GetAPIUser().displayName}");
                });
            return false;
        }

        private static bool AntiPublicBan(ref string __0, ref int __1, ref Player __2)
        {
            if (Configuration.GetConfig().LogModerations)
                HudPrint(__0, __2, (sender, target, isyou) =>
                {
                    if (isyou)
                        GeneralUtils.InformHudText(Color.red, $"You were attempt public banned by {sender.GetAPIUser().displayName}");
                    else
                        GeneralUtils.InformHudText(Color.red, $"{target.GetAPIUser().displayName} has been public banned by {sender.GetAPIUser().displayName}");
                });
            return !Configuration.GetConfig().AntiPublicBan;
        }

        private static bool BanPatch(ref string __0, ref int __1, ref Player __2)
        {
            if (Configuration.GetConfig().LogModerations)
                HudPrint(__0, __2, (sender, target, isyou) =>
                {
                    if (isyou)
                        GeneralUtils.InformHudText(Color.red, $"You were banned by {sender.GetAPIUser().displayName}");
                    else
                        GeneralUtils.InformHudText(Color.red, $"{target.GetAPIUser().displayName} has been banned by {sender.GetAPIUser().displayName}");
                });
            return true;
        }

        private static bool FriendPatch(ref string __0, ref Player __1)
        {
            if (Configuration.GetConfig().LogModerations)
                HudPrint(__0, __1, (sender, target, isyou) =>
                {
                    if (isyou)
                        GeneralUtils.InformHudText(Color.red, $"You were friended/unfriended by {sender.GetAPIUser().displayName}"); //no real way to check either lol
                    else
                        GeneralUtils.InformHudText(Color.red, $"{target.GetAPIUser().displayName} has been friended/unfriended by {sender.GetAPIUser().displayName}");
                });
            return true;
        }

        private static bool MutePatch(ref string __0, bool __1, ref Player __2)
        {
            if (Configuration.GetConfig().LogModerations)
                HudPrint(__0, __2, (sender, target, isyou) =>
                {
                    if (isyou)
                        GeneralUtils.InformHudText(Color.red, $"You were {(__1 ? "muted" : "unmuted")} by {sender.GetAPIUser().displayName}");
                    else
                        GeneralUtils.InformHudText(Color.red, $"{target.GetAPIUser().displayName} has been {(__1 ? "muted" : "unmuted")} by {sender.GetAPIUser().displayName}");
                });
            return true;
        }

        private static bool AvatarShownPatch(ref string __0, bool __1, ref Player __2)
        {
            if (Configuration.GetConfig().LogModerations)
                HudPrint(__0, __2, (sender, target, isyou) =>
                {
                    if (isyou)
                        GeneralUtils.InformHudText(Color.red, $"You were {(__1 ? "shown" : "hidden")} by {sender.GetAPIUser().displayName}");
                    else
                        GeneralUtils.InformHudText(Color.red, $"{target.GetAPIUser().displayName} has been {(__1 ? "shown" : "hidden")} by {sender.GetAPIUser().displayName}");
                });
            return true;
        }

        private static bool WarnPatch(ref string __0, ref string __1, ref Player __2)
        {
            if (Configuration.GetConfig().LogModerations)
                HudPrint(__0, __2, (sender, target, isyou) =>
                {
                    if (isyou)
                        GeneralUtils.InformHudText(Color.red, $"You were warned by {sender.GetAPIUser().displayName}");
                    else
                        GeneralUtils.InformHudText(Color.red, $"{target.GetAPIUser().displayName} has been warned by {sender.GetAPIUser().displayName}");
                });
            return true;
        }

        public static void HudPrint(string uid, Player ply, Action<Player, Player, bool> dothis)
        {
            if (ply == null)
                return;
            if (APIUser.CurrentUser == null)
                return;
            if (string.IsNullOrEmpty(uid))
                return;
            if (ply.GetAPIUser() == null)
                return;
            var target = GeneralWrappers.GetPlayerManager().GetPlayer(uid);
            if (target == null)
                return;
            if (target.GetAPIUser() == null)
                return;
            dothis?.Invoke(ply, target, (target.GetAPIUser().id == APIUser.CurrentUser.id));
        }

        private static bool ModForceOffMicPatch(ref string __0, ref Player __1)
        {
            if (Configuration.GetConfig().LogModerations)
                HudPrint(__0, __1, (sender, target, isyou) =>
                {
                    if (isyou)
                        GeneralUtils.InformHudText(Color.red, $"Your microphone was attempt forced off by {sender.GetAPIUser().displayName}");
                    else
                        GeneralUtils.InformHudText(Color.red, $"{target.GetAPIUser().displayName} has had their microphone forced off by {sender.GetAPIUser().displayName}");
                });
            return false;
        }
    }
    #endregion
}
