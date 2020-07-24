using FuneralClientV2.Discord;
using FuneralClientV2.Menu;
using FuneralClientV2.Modules;
using FuneralClientV2.Patching;
using FuneralClientV2.Settings;
using FuneralClientV2.Utils;
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

namespace FuneralClientV2
{
    public class FuneralClient : MelonMod
    {
        public static List<VRCMod> Modules = new List<VRCMod>();

        public override void VRChat_OnUiManagerInit()
        {
            try
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
                for (int i = 0; i < Modules.Count; i++)
                    Modules[i].OnUiLoad();
                try
                {
                    //DiscordCRPC.Start();
                    //DiscordRPC.Start();
                }
                catch(Exception c)
                {
                    ConsoleUtil.Error(c.ToString());
                    ConsoleUtil.Exception(c);
                }
            }
            catch (Exception) { }
        }

        public override void OnApplicationQuit()
        {
            //if (DiscordCRPC.client != null)
            //    DiscordCRPC.client.Dispose();
            for (int i = 0; i < Modules.Count; i++)
                Modules[i].OnAppQuit();
        }
        public override void OnApplicationStart()
        {
            try
            {

                PatchManager.ApplyPatches(); // Switch for earliest possible patching.
            } catch(Exception c)
            {
                ConsoleUtil.Exception(c);
            }
            try
            {
                ConsoleUtil.SetTitle("Funeral Client V2 = Developed by Yaekith & 404#0004");
                Configuration.CheckExistence();
                Modules.Add(new GeneralHandlers());
                ConsoleUtil.Info("Waiting for VRChat UI Manager to Initialise..");
                for (int i = 0; i < Modules.Count; i++)
                    Modules[i].OnStart();
            }
            catch (Exception e) { ConsoleUtil.Exception(e); }
        }

        public override void OnUpdate()
        {
            for (int i = 0; i < Modules.Count; i++)
                Modules[i].OnUpdate();
        }
    }
}
