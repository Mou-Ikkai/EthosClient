using EthosClient.Settings;
using EthosClient.Utils;
using EthosClient.Wrappers;
using Harmony;
using Il2CppSystem.Runtime.Remoting.Messaging;
using Il2CppSystem.Security.Cryptography;
using MelonLoader;
using Photon.Pun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnhollowerBaseLib;
using UnityEngine;
using VRC;
using VRC.Core;
using VRC.UI;
using VRCSDK2;
using static VRC.SDKBase.VRC_EventHandler;

namespace EthosClient.Patching
{
    public static class PatchManager
    {
        private static HarmonyMethod GetLocalPatch(string name) { return new HarmonyMethod(typeof(PatchManager).GetMethod(name, BindingFlags.Static | BindingFlags.NonPublic)); }

        private static List<Patch> RetrievePatches()
        {
            var ConsoleWriteLine = AccessTools.Method(typeof(Il2CppSystem.Console), "WriteLine", new Type[] { typeof(string) });
            List <Patch> patches = new List<Patch>()
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
                new Patch("Ethos_Moderation", typeof(VRC_EventDispatcherRFC).GetMethod("Method_Private_Void_Int32_VrcTargetType_GameObject_String_ArrayOf_Byte_0"), GetLocalPatch("InterceptRPC"), null),
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
            if (GeneralUtils.WorldTriggers) __1 = VrcBroadcastType.Always; // really scuffed yaekith we need to fix this. lol - 404
            else if (Configuration.GetConfig().AntiWorldTriggers && __1 == VrcBroadcastType.Always) return false; //Anti World triggers lol
            return true;
        }

        private static bool AntiKick(ref string __0, ref string __1, ref string __2, ref string __3, ref Player __4)
        {
            //to-do; add support for moderation logging
            var target = GeneralWrappers.GetPlayerManager().GetPlayer(__0);
            var them = __4.GetAPIUser();
            if (target.GetAPIUser().id == PlayerWrappers.GetCurrentPlayer().GetVRC_Player().GetAPIUser().id)
                if (Configuration.GetConfig().LogModerations) GeneralUtils.InformHudText(Color.red, $"You were attempt kicked by {them.displayName}");
            else
                if (Configuration.GetConfig().LogModerations) GeneralUtils.InformHudText(Color.red, $"{target.GetAPIUser().displayName} has been kicked by {them.displayName}");

            return !Configuration.GetConfig().AntiKick;
        }

        private static bool AntiBlock(ref string __0, ref bool __1, ref Player __2)
        {
            //to-do; add support for moderation logging
            var target = GeneralWrappers.GetPlayerManager().GetPlayer(__0);
            var them = __2.GetAPIUser();
            if (target.GetAPIUser().id == PlayerWrappers.GetCurrentPlayer().GetVRC_Player().GetAPIUser().id)
                if (Configuration.GetConfig().LogModerations) GeneralUtils.InformHudText(Color.red, $"You were {(__1 ? "blocked" : "unblocked")} by {them.displayName}");
            else
                if (Configuration.GetConfig().LogModerations) GeneralUtils.InformHudText(Color.red, $"{target.GetAPIUser().displayName} has been {(__1 ? "blocked" : "unblocked")} by {them.displayName}");

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
            if (Configuration.GetConfig().AntiPublicBan)
            {
                __result = false;
                return false;
            } else
            { return true; }
        }

        public static bool AntiLogout(ref string __0, ref Player __1)
        {
            var target = GeneralWrappers.GetPlayerManager().GetPlayer(__0);
            var them = __1.GetAPIUser();
            if (target.GetAPIUser().id == PlayerWrappers.GetCurrentPlayer().GetVRC_Player().GetAPIUser().id)
                if (Configuration.GetConfig().LogModerations) GeneralUtils.InformHudText(Color.red, $"You were attempt logged out by {them.displayName}");
            else
                if (Configuration.GetConfig().LogModerations) GeneralUtils.InformHudText(Color.red, $"{target.GetAPIUser().displayName} has been logged out by {them.displayName}");

            return false;
        }

        public static bool AntiPublicBan(ref string __0, ref int __1, ref Player __2)
        {
            var target = GeneralWrappers.GetPlayerManager().GetPlayer(__0);
            var them = __2.GetAPIUser();
            if (target.GetAPIUser().id == PlayerWrappers.GetCurrentPlayer().GetVRC_Player().GetAPIUser().id)
                if (Configuration.GetConfig().LogModerations) GeneralUtils.InformHudText(Color.red, $"You were attempt public banned by {them.displayName}");
            else
                if (Configuration.GetConfig().LogModerations) GeneralUtils.InformHudText(Color.red, $"{target.GetAPIUser().displayName} has been public banned by {them.displayName}");

            return !Configuration.GetConfig().AntiPublicBan;
        }

        public static bool BanPatch(ref string __0, ref int __1, ref Player __2)
        {
            var target = GeneralWrappers.GetPlayerManager().GetPlayer(__0);
            var them = __2.GetAPIUser();
            if (target.GetAPIUser().id == PlayerWrappers.GetCurrentPlayer().GetVRC_Player().GetAPIUser().id)
                if (Configuration.GetConfig().LogModerations) GeneralUtils.InformHudText(Color.red, $"You were banned by {them.displayName}");
            else
                if (Configuration.GetConfig().LogModerations) GeneralUtils.InformHudText(Color.red, $"{target.GetAPIUser().displayName} has been banned by {them.displayName}");

            return true;
        }

        public static bool FriendPatch(ref string __0, ref Player __1)
        {
            var target = GeneralWrappers.GetPlayerManager().GetPlayer(__0);
            var them = __1.GetAPIUser();
            if (target.GetAPIUser().id == PlayerWrappers.GetCurrentPlayer().GetVRC_Player().GetAPIUser().id)
                if (Configuration.GetConfig().LogModerations) GeneralUtils.InformHudText(Color.red, $"You were friended/unfriended by {them.displayName}"); //no real way to check either lol
            else
                if (Configuration.GetConfig().LogModerations) GeneralUtils.InformHudText(Color.red, $"{target.GetAPIUser().displayName} has been friended/unfriended by {them.displayName}");

            return true;
        }

        public static bool MutePatch(ref string __0, ref bool __1, ref Player __2)
        {
            var target = GeneralWrappers.GetPlayerManager().GetPlayer(__0);
            var them = __2.GetAPIUser();
            if (target.GetAPIUser().id == PlayerWrappers.GetCurrentPlayer().GetVRC_Player().GetAPIUser().id)
                if (Configuration.GetConfig().LogModerations) GeneralUtils.InformHudText(Color.red, $"You were {(__1 ? "muted" : "unmuted")} by {them.displayName}");
            else
                if (Configuration.GetConfig().LogModerations) GeneralUtils.InformHudText(Color.red, $"{target.GetAPIUser().displayName} has been {(__1 ? "muted" : "unmuted")} by {them.displayName}");

            return true;
        }

        public static bool AvatarShownPatch(ref string __0, ref bool __1, ref Player __2)
        {
            var target = GeneralWrappers.GetPlayerManager().GetPlayer(__0);
            var them = __2.GetAPIUser();
            if (target.GetAPIUser().id == PlayerWrappers.GetCurrentPlayer().GetVRC_Player().GetAPIUser().id)
                if (Configuration.GetConfig().LogModerations) GeneralUtils.InformHudText(Color.red, $"You were {(__1 ? "shown" : "hidden")} by {them.displayName}");
            else
                if (Configuration.GetConfig().LogModerations) GeneralUtils.InformHudText(Color.red, $"{target.GetAPIUser().displayName} has been {(__1 ? "shown" : "hidden")} by {them.displayName}");

            return true;
        }

        public static bool WarnPatch(ref string __0, ref string __1, ref Player __2)
        {
            var target = GeneralWrappers.GetPlayerManager().GetPlayer(__0);
            var them = __2.GetAPIUser();
            if (target.GetAPIUser().id == PlayerWrappers.GetCurrentPlayer().GetVRC_Player().GetAPIUser().id)
                if (Configuration.GetConfig().LogModerations) GeneralUtils.InformHudText(Color.red, $"You were warned by {them.displayName}");
            else
                if (Configuration.GetConfig().LogModerations) GeneralUtils.InformHudText(Color.red, $"{target.GetAPIUser().displayName} has been warned by {them.displayName}");

            return true;
        }

        public static bool ModForceOffMicPatch(ref string __0, ref Player __1)
        {
            var target = GeneralWrappers.GetPlayerManager().GetPlayer(__0);
            var them = __1.GetAPIUser();
            if (target.GetAPIUser().id == PlayerWrappers.GetCurrentPlayer().GetVRC_Player().GetAPIUser().id)
                if (Configuration.GetConfig().LogModerations) GeneralUtils.InformHudText(Color.red, $"Your microphone was attempt forced off by {them.displayName}");
            else
                if (Configuration.GetConfig().LogModerations) GeneralUtils.InformHudText(Color.red, $"{target.GetAPIUser().displayName} has had their microphone forced off by {them.displayName}");

            return false;
        }

        private static bool InterceptRpc(int __0, VRC.SDKBase.VRC_EventHandler.VrcTargetType __1, string __3, Il2CppStructArray<byte> __4)
        {
            try
            {
                Player sender = PlayerManager.Method_Public_Static_Player_Int32_0(__0);
                Il2CppSystem.Object[] array = VrcSdk2Interface.ObjectCompilerGeneratedNPrivateSealedObFu2VRBoAcFu2VRBoUnique.field_Public_Static_ObjectCompilerGeneratedNPrivateSealedObFu2VRBoAcFu2VRBoUnique_0.Method_Internal_ArrayOf_Object_ArrayOf_Byte_0(__4);
                string receiver = APIUser.CurrentUser.id;
                if (array.Length >= 1 && receiver.Length > 10 && receiver != "0") receiver = array[0].ToString();
                if (Configuration.GetConfig().LogModerations) ConsoleUtil.Info($"[{__3}] Sent to {GeneralWrappers.GetPlayerManager().GetPlayer(receiver).GetAPIUser().displayName} from {sender.GetAPIUser().displayName}");
                return true;
            }
            catch { return true; }
        }
    }
    #endregion
}
