using EthosClient.Settings;
using EthosClient.Utils;
using EthosClient.Wrappers;
using Harmony;
using Photon.Pun;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using VRC;
using VRC.SDKBase;
using static VRC.SDKBase.VRC_EventHandler;

namespace EthosClient.Patching
{
    public static class PatchManager
    {
        private static List<string> PlayerCache = new List<string>();

        private static HarmonyMethod GetLocalPatch(string name) { return new HarmonyMethod(typeof(PatchManager).GetMethod(name, BindingFlags.Static | BindingFlags.NonPublic)); }

        private static void RetrievePatches()
        {
            new Patch("Ethos_Extras", AccessTools.Method(typeof(VRC_EventHandler), "InternalTriggerEvent", null, null), GetLocalPatch("TriggerEvent"), null);
            new Patch("Ethos_Moderation", typeof(ModerationManager).GetMethod("KickUserRPC"), GetLocalPatch("AntiKick"), null);
            new Patch("Ethos_Moderation", typeof(ModerationManager).GetMethod("Method_Public_Boolean_String_String_String_1"), GetLocalPatch("CanEnterPublicWorldsPatch"), null);
            new Patch("Ethos_Moderation", typeof(ModerationManager).GetMethod("BlockStateChangeRPC"), GetLocalPatch("AntiBlock"), null);
            new Patch("Ethos_Moderation", typeof(ModerationManager).GetMethod("ForceLogoutRPC"), GetLocalPatch("AntiLogout"), null);
            new Patch("Ethos_Moderation", typeof(ModerationManager).GetMethod("BanPublicOnlyRPC"), GetLocalPatch("AntiPublicBan"), null);
            new Patch("Ethos_Moderation", typeof(ModerationManager).GetMethod("FriendStateChangeRPC"), GetLocalPatch("FriendPatch"), null);
            new Patch("Ethos_Moderation", typeof(ModerationManager).GetMethod("BanRPC"), GetLocalPatch("BanPatch"), null);
            new Patch("Ethos_Moderation", typeof(ModerationManager).GetMethod("MuteChangeRPC"), GetLocalPatch("MutePatch"), null);
            new Patch("Ethos_Moderation", typeof(ModerationManager).GetMethod("ShowUserAvatarChangedRPC"), GetLocalPatch("AvatarShownPatch"), null);
            new Patch("Ethos_Moderation", typeof(ModerationManager).GetMethod("WarnUserRPC"), GetLocalPatch("WarnPatch"), null);
            new Patch("Ethos_Moderation", typeof(ModerationManager).GetMethod("ModForceOffMicRPC"), GetLocalPatch("ModForceOffMicPatch"), null);
            new Patch("Ethos_Moderation", typeof(VRC_TriggerInternal).GetMethod("OnPlayerJoined"), GetLocalPatch("OnPlayerJoin"), null);
            new Patch("Ethos_Moderation", typeof(VRC_TriggerInternal).GetMethod("OnPlayerLeft"), GetLocalPatch("OnPlayerLeave"), null);
            new Patch("Ethos_Extras", typeof(UserInteractMenu).GetMethod("Update"), GetLocalPatch("CloneAvatarPrefix"), null);
            new Patch("Ethos_Extras", AccessTools.Method(typeof(Il2CppSystem.Console), "WriteLine", new Type[] { typeof(string) }), GetLocalPatch("IL2CPPConsoleWriteLine"), null);
            new Patch("Ethos_Extras", typeof(ImageDownloader).GetMethod("DownloadImage"), GetLocalPatch("AntiIpLogImage"), null);
            new Patch("Ethos_Extras", typeof(VRCSDK2.VRC_SyncVideoPlayer).GetMethod("AddURL"), GetLocalPatch("AntiVideoPlayerHijacking"), null);
            new Patch("Ethos_Extras", typeof(PhotonView).GetMethod("Method_Public_Void_ObjectPublicQu1ObByObBoInBoBoUnique_ValueTypePublicSealedInObPhDoInUnique_0"), GetLocalPatch("SerializeView"), null);
            new Patch("Ethos_Extras", typeof(PhotonView).GetMethod("Method_Public_Void_ObjectPublicQu1ObByObBoInBoBoUnique_ValueTypePublicSealedInObPhDoInUnique_1"), GetLocalPatch("SerializeView"), null);
            new Patch("Ethos_Extras", typeof(PhotonView).GetMethod("Method_Public_Void_ObjectPublicQu1ObByObBoInBoBoUnique_ValueTypePublicSealedInObPhDoInUnique_2"), GetLocalPatch("SerializeView"), null);
            new Patch("Ethos_Extras", typeof(PhotonView).GetMethod("Method_Public_Void_ObjectPublicQu1ObByObBoInBoBoUnique_ValueTypePublicSealedInObPhDoInUnique_3"), GetLocalPatch("SerializeView"), null);
            new Patch("Ethos_Extras", typeof(PhotonView).GetMethod("Method_Public_Void_ObjectPublicQu1ObByObBoInBoBoUnique_ValueTypePublicSealedInObPhDoInUnique_4"), GetLocalPatch("SerializeView"), null);
        }

        public static void ApplyPatches()
        {
            RetrievePatches();
            ConsoleUtil.Info("All Patches have been applied successfully.");
        }

        #region Patches
        private static bool TriggerEvent(ref VrcEvent __0, ref VrcBroadcastType __1, ref int __2, ref float __3)
        {
            List<string> FilteredStrings = new List<string>()
            {
                "Mirror",
                "Chair",
                "Wall",
                "Option",
                "Box Capsule",
                "Lounge",
                "Camp",
                "Skybox"                        
            };
            bool isFiltered = false;

            for (var i = 0; i < FilteredStrings.Count; i++)
            {
                if (FilteredStrings[i].ToLower().Contains(__0.ParameterObject.name.ToLower().ToString()))
                    isFiltered = true;
            }

            if (GeneralUtils.IsDevBranch)
                Console.WriteLine(__0.ParameterObject.name + " - " + __2 + " - " + __3);

            if (__1 == VrcBroadcastType.Always || __1 == VrcBroadcastType.AlwaysUnbuffered)
            {
                if (Configuration.GetConfig().AntiWorldTriggers && isFiltered)
                    return false;

                else if (Configuration.GetConfig().AntiTriggers)
                    return false;
            }

            if (GeneralUtils.WorldTriggers)
                __1 = VrcBroadcastType.Always;

            return true;
        }

        private static bool SerializeView()
        {
            return !GeneralUtils.CustomSerialization;
        }

        private static bool OnPlayerJoin(ref VRCPlayerApi __0)
        {
            if (__0 != null)
            {
                if (GeneralUtils.WhitelistedCanHearUsers.Contains(__0.displayName))
                    GeneralUtils.WhitelistedCanHearUsers.Remove(__0.displayName); //just to be sure they dont already exist (i could do this in the playereventshandler but this one is better and is called more)

                if (!PlayerCache.Contains(__0.displayName))
                {
                    for (var i = 0; i < GeneralUtils.Modules.Count; i++)
                        GeneralUtils.Modules[i].OnPlayerJoin(__0);

                    PlayerCache.Add(__0.displayName);
                }
            }
            return true;
        }

        private static bool OnPlayerLeave(ref VRCPlayerApi __0)
        {
            if (__0 != null)
            {
                if (PlayerCache.Contains(__0.displayName))
                {
                    for (var i = 0; i < GeneralUtils.Modules.Count; i++)
                        GeneralUtils.Modules[i].OnPlayerLeft(__0);

                    PlayerCache.Remove(__0.displayName);
                }
            }
            return true;
        }

        private static bool AntiKick(ref string __0, ref string __1, ref string __2, ref string __3, ref Player __4)
        {
            if (GeneralWrappers.GetPlayerManager().GetPlayer(__0) != null)
            {
                for (var i = 0; i < GeneralUtils.Modules.Count; i++)
                    GeneralUtils.Modules[i].OnPlayerKicked(GeneralWrappers.GetPlayerManager().GetPlayer(__0), __4);
            }
            return !Configuration.GetConfig().AntiKick;
        }

        private static bool AntiBlock(ref string __0, bool __1, ref Player __2)
        {
            if (GeneralWrappers.GetPlayerManager().GetPlayer(__0) != null)
            {
                for (var i = 0; i < GeneralUtils.Modules.Count; i++)
                    GeneralUtils.Modules[i].OnPlayerBlocked(GeneralWrappers.GetPlayerManager().GetPlayer(__0), __2, __1);
            }
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
            if (GeneralWrappers.GetPlayerManager().GetPlayer(__0) != null)
            {
                for (var i = 0; i < GeneralUtils.Modules.Count; i++)
                    GeneralUtils.Modules[i].OnPlayerLoggedOut(GeneralWrappers.GetPlayerManager().GetPlayer(__0), __1);
            }
            return false;
        }

        private static bool AntiPublicBan(ref string __0, ref int __1, ref Player __2)
        {
            if (GeneralWrappers.GetPlayerManager().GetPlayer(__0) != null)
            {
                for (var i = 0; i < GeneralUtils.Modules.Count; i++)
                    GeneralUtils.Modules[i].OnPlayerPublicBanned(GeneralWrappers.GetPlayerManager().GetPlayer(__0), __2);
            }
            return !Configuration.GetConfig().AntiPublicBan;
        }

        private static bool BanPatch(ref string __0, ref int __1, ref Player __2)
        {
            if (GeneralWrappers.GetPlayerManager().GetPlayer(__0) != null)
            {
                for (var i = 0; i < GeneralUtils.Modules.Count; i++)
                    GeneralUtils.Modules[i].OnPlayerBanned(GeneralWrappers.GetPlayerManager().GetPlayer(__0), __2);
            }
            return true;
        }

        private static bool FriendPatch(ref string __0, ref Player __1)
        {
            if (GeneralWrappers.GetPlayerManager().GetPlayer(__0) != null)
            {
                for (var i = 0; i < GeneralUtils.Modules.Count; i++)
                    GeneralUtils.Modules[i].OnPlayerFriended(GeneralWrappers.GetPlayerManager().GetPlayer(__0), __1);
            }
            return true;
        }

        private static bool MutePatch(ref string __0, bool __1, ref Player __2)
        {
            if (GeneralWrappers.GetPlayerManager().GetPlayer(__0) != null)
            {
                for (var i = 0; i < GeneralUtils.Modules.Count; i++)
                    GeneralUtils.Modules[i].OnPlayerMuted(GeneralWrappers.GetPlayerManager().GetPlayer(__0), __2, __1);
            }
            return true;
        }

        private static bool AvatarShownPatch(ref string __0, bool __1, ref Player __2)
        {
            if (GeneralWrappers.GetPlayerManager().GetPlayer(__0) != null)
            {
                for (var i = 0; i < GeneralUtils.Modules.Count; i++)
                    GeneralUtils.Modules[i].OnPlayerShown(GeneralWrappers.GetPlayerManager().GetPlayer(__0), __2, __1);
            }
            return true;
        }

        private static bool WarnPatch(ref string __0, ref string __1, ref Player __2)
        {
            if (GeneralWrappers.GetPlayerManager().GetPlayer(__0) != null)
            {
                for (var i = 0; i < GeneralUtils.Modules.Count; i++)
                    GeneralUtils.Modules[i].OnPlayerWarned(GeneralWrappers.GetPlayerManager().GetPlayer(__0), __2);
            }
            return true;
        }

        private static bool ModForceOffMicPatch(ref string __0, ref Player __1)
        {
            if (GeneralWrappers.GetPlayerManager().GetPlayer(__0) != null)
            {
                for (var i = 0; i < GeneralUtils.Modules.Count; i++)
                    GeneralUtils.Modules[i].OnPlayerWarned(GeneralWrappers.GetPlayerManager().GetPlayer(__0), __1);
            }
            return true;
        }
    }
    #endregion
}
