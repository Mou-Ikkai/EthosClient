using EthosClient.Discord;
using EthosClient.Menu;
using EthosClient.Modules;
using EthosClient.Patching;
using EthosClient.Settings;
using EthosClient.Utils;
using Il2CppSystem.Reflection;
using MelonLoader;
using RubyButtonAPI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace EthosClient
{
    public class EthosClient : MelonMod
    {
        private static List<VRCMod> Modules = new List<VRCMod>();

        public override void VRChat_OnUiManagerInit()
        {
            #region Adding Main Menu Buttons
            new MainMenu(GeneralUtils.GetEthosVRButton("MainMenu"));
            #endregion
            #region Creating Side UI Menu
            Transform background = QuickMenu.prop_QuickMenu_0.transform.Find("QuickMenu_NewElements/_Background/Panel");
            Transform sidemenu = UnityEngine.Object.Instantiate(background, QuickMenu.prop_QuickMenu_0.transform.Find("QuickMenu_NewElements/_Background"));
            QuickMenu.prop_QuickMenu_0.GetComponent<BoxCollider>().size += QuickMenu.prop_QuickMenu_0.GetComponent<BoxCollider>().size;
            sidemenu.GetComponent<RectTransform>().anchoredPosition -= new Vector2(sidemenu.GetComponent<RectTransform>().anchoredPosition.x * 12, -410);
            sidemenu.transform.localScale = new Vector3(sidemenu.transform.localScale.x / 2, sidemenu.transform.localScale.y, sidemenu.transform.localScale.z);
            #endregion
            #region Creating Instances Of Buttons
            var Menu = new QuickMenuButton();
            #endregion
            #region Modifying Button Sizes
            Menu.getMainButton().getGameObject().GetComponent<RectTransform>().sizeDelta /= new Vector2(0.75f, 2.5f);
            #endregion
            #region Anchoring Button Positions
            Menu.getMainButton().getGameObject().GetComponent<RectTransform>().anchoredPosition += Vector2.down * (-210);
            Menu.getMainButton().getGameObject().GetComponent<RectTransform>().anchoredPosition -= Vector2.right * (420 * (0.625f)); 
            #endregion
            #region Setting Button Parents To Side UI
            Menu.getMainButton().getGameObject().transform.parent = sidemenu;
            #endregion
            #region Setting up Discord Rich Presence
            DiscordRPC.Start();
            #endregion
            #region Loading Modules
            for (int i = 0; i < Modules.Count; i++)
                Modules[i].OnUiLoad();
            #endregion
        }

        public override void OnApplicationQuit()
        {
            #region Disposing of all custom modules respectively
            for (int i = 0; i < Modules.Count; i++)
                Modules[i].OnAppQuit();
            #endregion
        }

        public override void OnApplicationStart()
        {
            #region Exception Handling System of Unexpected Errors
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            #endregion
            #region Applying Game Patches
            PatchManager.ApplyPatches();
            #endregion
            #region Setting Console Title
            ConsoleUtil.SetTitle("Ethos Client = Developed by Yaekith & 404#0004");
            #endregion
            #region Checking Existence of Configuration and Adding Modules to list
            Configuration.CheckExistence();
            Modules.Add(new GeneralHandlers());
            ConsoleUtil.Info("Waiting for VRChat UI Manager to Initialise..");
            #endregion
            #region Starting Modules
            for (int i = 0; i < Modules.Count; i++)
                Modules[i].OnStart();
            #endregion
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e) => ConsoleUtil.Exception(e.ExceptionObject as Exception);

        public override void OnUpdate() 
        {
            #region On Module Updates
            for (int i = 0; i < Modules.Count; i++)
                Modules[i].OnUpdate();
            #endregion
        }
    }
}
