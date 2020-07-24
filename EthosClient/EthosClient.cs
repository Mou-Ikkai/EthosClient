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
        public static List<VRCMod> Modules = new List<VRCMod>();

        public override void VRChat_OnUiManagerInit()
        {
            ConsoleUtil.Info("[DEBUG] VRChat_OnUiManagerInit callback was fired.");
            new QMSingleButton("UIElementsMenu", 1, 1, "Move Menu Button\nLeft", new Action(() =>
            {
                if (Configuration.GetConfig().MainMenuButtonX != 1)
                {
                    Configuration.GetConfig().MainMenuButtonX--;
                    Configuration.SaveConfiguration();
                    GeneralUtils.InformHudText(Color.yellow, "Successfully saved menu button position\nRestart your game for it to take effect.");
                }
            }), "Moves the main menu button to the left within the UI", Color.red, Color.white);
            new QMSingleButton("UIElementsMenu", 1, 2, "Move Menu Button\nRight", new Action(() =>
            {
                if (Configuration.GetConfig().MainMenuButtonX != 1)
                {
                    Configuration.GetConfig().MainMenuButtonX++;
                    Configuration.SaveConfiguration();
                    GeneralUtils.InformHudText(Color.yellow, "Successfully saved menu button position\nRestart your game for it to take effect.");
                }
            }), "Moves the main menu button to the right within the UI", Color.red, Color.white);
            new QMSingleButton("UIElementsMenu", 2, 1, "Move Menu Button\nUp", new Action(() =>
            {
                if (Configuration.GetConfig().MainMenuButtonY != 0)
                {
                    Configuration.GetConfig().MainMenuButtonY--;
                    Configuration.SaveConfiguration();
                    GeneralUtils.InformHudText(Color.yellow, "Successfully saved menu button position\nRestart your game for it to take effect.");
                }
            }), "Moves the main menu button up within the UI", Color.red, Color.white);
            new QMSingleButton("UIElementsMenu", 2, 2, "Move Menu Button\nDown", new Action(() =>
            {
                if (Configuration.GetConfig().MainMenuButtonY != 4)
                {
                    Configuration.GetConfig().MainMenuButtonY++;
                    Configuration.SaveConfiguration();
                    GeneralUtils.InformHudText(Color.yellow, "Successfully saved menu button position\nRestart your game for it to take effect.");
                }
            }), "Moves the main menu button up within the UI", Color.red, Color.white);
            new MainMenu();
            DiscordRPC.Start();
            for (int i = 0; i < Modules.Count; i++) Modules[i].OnUiLoad();
        }

        public override void OnApplicationQuit() { for (int i = 0; i < Modules.Count; i++) Modules[i].OnAppQuit(); }

        public override void OnApplicationStart()
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            AppDomain.CurrentDomain.FirstChanceException += CurrentDomain_FirstChanceException;
            PatchManager.ApplyPatches();
            ConsoleUtil.SetTitle("Ethos Client = Developed by Yaekith & 404#0004");
            Configuration.CheckExistence();
            Modules.Add(new GeneralHandlers());
            ConsoleUtil.Info("Waiting for VRChat UI Manager to Initialise..");
            for (int i = 0; i < Modules.Count; i++) Modules[i].OnStart();
        }

        private void CurrentDomain_FirstChanceException(object sender, System.Runtime.ExceptionServices.FirstChanceExceptionEventArgs e) => ConsoleUtil.Exception(e.Exception);

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e) => ConsoleUtil.Exception(e.ExceptionObject as Exception);

        public override void OnUpdate() { for (int i = 0; i < Modules.Count; i++) Modules[i].OnUpdate(); }
    }
}
