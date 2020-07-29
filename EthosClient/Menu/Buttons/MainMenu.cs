using EthosClient.Settings;
using EthosClient.Utils;
using EthosClient.Wrappers;
using Il2CppSystem.Threading;
using RubyButtonAPI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace EthosClient.Menu
{
    public class MainMenu : QMNestedButton
    {
        public MainMenu(EthosVRButton config) : base(config.Menu, config.X, config.Y, config.Name, config.Tooltip, GeneralUtils.GetColor(config.ColorScheme.Colors[0]), GeneralUtils.GetColor(config.ColorScheme.Colors[1]), GeneralUtils.GetColor(config.ColorScheme.Colors[2]), GeneralUtils.GetColor(config.ColorScheme.Colors[3]))
        {
            new QMSingleButton(this, 1, 0, "GitHub", new Action(() =>
            {
                Process.Start("https://github.com/Yaekith/EthosClient");
            }), "Open the github repository in a new browser window", Color.red, Color.white);
            new QMSingleButton(this, 2, 0, "Discord", new Action(() =>
            {
                Process.Start("https://discord.gg/8fwurVW");
            }), "Join the official discord", Color.red, Color.white);
            new QMSingleButton(this, 3, 0, "Credits", new Action(() =>
            {
                GeneralUtils.InformHudText(Color.yellow, "Yaekith - Developer\n404 - Developer");
            }), "Displays who made this cheat", Color.red, Color.white);
            new UtilsVRMenu(this, GeneralUtils.GetEthosVRButton("Utils"));
            new FunVRMenu(this, GeneralUtils.GetEthosVRButton("Fun"));
            new ProtectionsVRMenu(this, GeneralUtils.GetEthosVRButton("Protections"));
            new TargetVRMenu(GeneralUtils.GetEthosVRButton("PlayerOptions"));
            new FavoritesVRMenu(this, GeneralUtils.GetEthosVRButton("ExtendedFavorites"));
            new SettingsVRMenu(this, GeneralUtils.GetEthosVRButton("Settings"));
            new KeybindVRMenu(this, GeneralUtils.GetEthosVRButton("Keybinds"));
            new VRUtilsMenu(this, GeneralUtils.GetEthosVRButton("VRUtils"));
            if (GeneralUtils.IsDevBranch) new DeveloperVRMenu(GeneralUtils.GetEthosVRButton("Developer"));
            new QMSingleButton(this, 4, 0, "Select\nYourself", new Action(() =>
            {
                GeneralWrappers.GetQuickMenu().SelectPlayer(PlayerWrappers.GetCurrentPlayer());
            }), "Select your own current player and do some stuff to yourself, I don't know lol.", Color.red, Color.white);
            new QMToggleButton(this, 1, 2, "Hide\nYourself", new Action(() =>
            {
                PlayerWrappers.GetCurrentPlayer().prop_VRCAvatarManager_0.gameObject.SetActive(false);
            }), "Unhide\nYourself", new Action(() =>
            {
                PlayerWrappers.GetCurrentPlayer().prop_VRCAvatarManager_0.gameObject.SetActive(true);
            }), "Hide/Unhide yourself, for safety reasons maybe, who knows.", Color.red, Color.white);
        }
    }
}
