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
    public class SettingsVRMenu : QMNestedButton
    {
        public SettingsVRMenu(QMNestedButton parent, EthosVRButton config) : base(parent, config.X, config.Y, config.Name, config.Tooltip, GeneralUtils.GetColor(config.ColorScheme.Colors[0]), GeneralUtils.GetColor(config.ColorScheme.Colors[1]), GeneralUtils.GetColor(config.ColorScheme.Colors[2]), GeneralUtils.GetColor(config.ColorScheme.Colors[3]))
        {
            new QMToggleButton(this, 1, 0, "Enable\nMenu RGB", delegate
            {
                Configuration.GetConfig().MenuRGB = true;
                Configuration.SaveConfiguration();
            }, "Disable\nMenu RGB", delegate
            {
                Configuration.GetConfig().MenuRGB = false;
                Configuration.SaveConfiguration();
            }, "Toggle whether you want your UI to change colors consistently (Rainbow UI mode basically)", Color.red, Color.white).setToggleState(Configuration.GetConfig().MenuRGB);
            new QMToggleButton(this, 2, 0, "Enable\nSide Menu", delegate
            {
                Configuration.GetConfig().SideUI = true;
                Configuration.SaveConfiguration();
            }, "Disable\nSide Menu", delegate
            {
                Configuration.GetConfig().SideUI = false;
                Configuration.SaveConfiguration();
            }, "Enable/Disable the Side UI (You know, the 'annoying' menu on the left lol)", Color.red, Color.white).setToggleState(Configuration.GetConfig().SideUI);
            new QMToggleButton(this, 3, 0, "Clear\nConsole", delegate
            {
                Configuration.GetConfig().CleanConsole = true;
                Configuration.SaveConfiguration();
            }, "Don't Clear\nConsole", delegate
            {
                Configuration.GetConfig().CleanConsole = false;
                Configuration.SaveConfiguration();
            }, "Decide whether you want your console to be spammed by useless game information or not.", Color.red, Color.white).setToggleState(Configuration.GetConfig().CleanConsole);
            new QMToggleButton(this, 4, 0, "Log\nTo Console", delegate
            {
                Configuration.GetConfig().DefaultLogToConsole = true;
            }, "Log\nTo HUD", delegate
            {
                Configuration.GetConfig().DefaultLogToConsole = false;
            }, "Decide whether you want to log all moderation/other client information to your console or your hud ingame.", Color.red, Color.white).setToggleState(Configuration.GetConfig().DefaultLogToConsole);
        }
    }
}
