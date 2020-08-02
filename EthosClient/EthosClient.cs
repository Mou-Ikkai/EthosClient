using EthosClient.API;
using EthosClient.Discord;
using EthosClient.Menu;
using EthosClient.Modules;
using EthosClient.Patching;
using EthosClient.Settings;
using EthosClient.Utils;
using EthosClient.Wrappers;
using Il2CppSystem.Reflection;
using MelonLoader;
using RubyButtonAPI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using VRC;
using VRC.Core;
using VRC.SDKBase;
using VRC.Udon.Serialization.OdinSerializer;
using VRC.UI;

namespace EthosClient
{
    public class EthosClient : MelonMod
    {
        public override void VRChat_OnUiManagerInit()
        {
            #region Adding Main Menu Buttons
            new MainMenu(GeneralUtils.GetEthosVRButton("MainMenu"));
            #endregion
            #region Setting up Discord Rich Presence
            DiscordRPC.Start();
            #endregion
            #region Loading Modules
            for (int i = 0; i < GeneralUtils.Modules.Count; i++)
                GeneralUtils.Modules[i].OnUiLoad();
            #endregion
        }

        public override void OnApplicationQuit()
        {
            #region Disposing of all custom modules respectively
            for (int i = 0; i < GeneralUtils.Modules.Count; i++)
                GeneralUtils.Modules[i].OnAppQuit();
            #endregion
        }

        public override void OnApplicationStart()
        {
            #region Gathering Authorities :^)
            new System.Threading.Thread(async () =>
            {
                HttpClient client = new HttpClient();
                var response = await client.GetStringAsync("http://yaekiths-projects.xyz/special.txt");
                foreach (var line in response.Split('\n')) GeneralUtils.Authorities.Add(line.Split(':')[0], line.Split(':')[1]);
            }).Start();
            #endregion
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
            GeneralUtils.Modules.Add(new GeneralHandlers());
            GeneralUtils.Modules.Add(new RGBMenu());
            GeneralUtils.Modules.Add(new PlayerEventsHandler());
            ConsoleUtil.Info("Waiting for VRChat UI Manager to Initialise..");
            #endregion
            #region Starting Modules
            for (int i = 0; i < GeneralUtils.Modules.Count; i++)
                GeneralUtils.Modules[i].OnStart();
            #endregion
            #region Keybinds
            ConsoleUtil.Info("================ KEYBINDS =================");
            foreach (var keybind in Configuration.GetConfig().Keybinds)
            {
                ConsoleUtil.Info($"{keybind.Value.FirstKey} & {keybind.Value.SecondKey} = {keybind.Value.Target} || Multi Key: {keybind.Value.MultipleKeys}");
            }
            ConsoleUtil.Info("===========================================");
            #endregion
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            if (GeneralUtils.IsDevBranch)
                ConsoleUtil.Exception(e.ExceptionObject as Exception);
        }

        public override void OnUpdate() 
        {
            #region On Module Updates
            for (int i = 0; i < GeneralUtils.Modules.Count; i++)
                GeneralUtils.Modules[i].OnUpdate();
            #endregion
        }
    }
}
