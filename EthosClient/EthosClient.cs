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
            new ButtonRepositionVRMenu();
            new MainMenu(GeneralUtils.GetEthosVRButton("MainMenu"));
            if (Configuration.GetConfig().UseRichPresence) DiscordRPC.Start(); //temp fix ok 404, not everyone can run without this line lol
            
            for (int i = 0; i < Modules.Count; i++)
                Modules[i].OnUiLoad();
        }

        public override void OnApplicationQuit()
        {
            for (int i = 0; i < Modules.Count; i++)
                Modules[i].OnAppQuit(); 
        }

        public override void OnApplicationStart()
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            PatchManager.ApplyPatches();
            ConsoleUtil.SetTitle("Ethos Client = Developed by Yaekith & 404#0004");
            Configuration.CheckExistence();
            Modules.Add(new GeneralHandlers());
            ConsoleUtil.Info("Waiting for VRChat UI Manager to Initialise..");

            for (int i = 0; i < Modules.Count; i++)
                Modules[i].OnStart();
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e) => ConsoleUtil.Exception(e.ExceptionObject as Exception);

        public override void OnUpdate() 
        { 
            for (int i = 0; i < Modules.Count; i++)
                Modules[i].OnUpdate();
        }
    }
}
