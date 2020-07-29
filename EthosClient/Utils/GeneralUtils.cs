using EthosClient.API;
using EthosClient.Menu;
using EthosClient.Modules;
using EthosClient.Settings;
using EthosClient.Wrappers;
using RubyButtonAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VRCSDK2;

namespace EthosClient.Utils
{
    public static class GeneralUtils
    {
        public static bool WorldTriggers = false;

        public static bool Flight = false;

        public static bool Autism = false;

        public static bool ESP = false;

        public static bool SpinBot = false;

        public static bool ForceClone = false;

        public static string Version = "1.5";

        public static bool IsDevBranch = false;

        public static Vector3 SavedGravity = Physics.gravity;

        public static List<VRCMod> Modules = new List<VRCMod>();

        public static void InformHudText(Color color, string text)
        {
            if (!Configuration.GetConfig().DefaultLogToConsole)
            {
                var NormalColor = VRCUiManager.prop_VRCUiManager_0.hudMessageText.color;
                VRCUiManager.prop_VRCUiManager_0.hudMessageText.color = color;
                VRCUiManager.prop_VRCUiManager_0.Method_Public_Void_String_0($"[ETHOS] {text}");
                VRCUiManager.prop_VRCUiManager_0.hudMessageText.color = NormalColor;
            }
            else ConsoleUtil.Info(text);
        }

        public static void ToggleColliders(bool toggle)
        {
            Collider[] array = UnityEngine.Object.FindObjectsOfType<Collider>();
            Component component = PlayerWrappers.GetCurrentPlayer().GetComponents<Collider>().FirstOrDefault<Component>();

            for (int i = 0; i < array.Length; i++)
            {
                Collider collider = array[i];
                bool flag = collider.GetComponent<PlayerSelector>() != null || collider.GetComponent<VRC.SDKBase.VRC_Pickup>() != null || collider.GetComponent<QuickMenu>() != null || collider.GetComponent<VRC_Station>() != null || collider.GetComponent<VRC.SDKBase.VRC_AvatarPedestal>() != null;
                if (!flag && collider != component) collider.enabled = toggle;
            }
        }

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[new System.Random().Next(s.Length)]).ToArray());
        }

        public static FavoritedAvatar GetExtendedFavorite(string ID)
        {
            foreach(var avatar in Configuration.GetConfig().ExtendedFavoritedAvatars) if (avatar.ID == ID) return avatar;
            return null;
        }

        public static bool SuitableVideoURL(string url)
        {
            if (url.Contains("youtube.com")) return true;
            else if (url.Contains("youtu.be")) return true;
            return false;
        }

        public static EthosVRButton GetEthosVRButton(string ID)
        {
            foreach(var button in Configuration.GetConfig().Buttons)
            {
                if (button.ID == ID)
                {
                    return button;
                }
            }
            return null;
        }

        public static EthosColor GetEthosColor(Color color) { return new EthosColor(color.r, color.g, color.b); }

        public static Color GetColor(EthosColor color) { return new Color(color.R, color.G, color.B); }
    }
}
