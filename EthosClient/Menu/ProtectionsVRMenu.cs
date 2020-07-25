using EthosClient.Settings;
using EthosClient.Wrappers;
using RubyButtonAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace EthosClient.Utils
{
    public class ProtectionsVRMenu : QMNestedButton
    {
        public ProtectionsVRMenu(QMNestedButton parent) : base(parent, 3, 1, "Protections", "A menu full of protection options against moderation, and other safety related features.", Color.red, Color.white, Color.red, Color.cyan)
        {
            new QMToggleButton(this, 1, 0, "Enable\nAnti Kick", delegate
            {
                Configuration.GetConfig().AntiKick = true;
                Configuration.SaveConfiguration();
            }, "Disable\nAnti Kick", delegate
            {
                Configuration.GetConfig().AntiKick = false;
                Configuration.SaveConfiguration();
            }, "Harvest the infinite power of this world and decide whether people can kick you from the instance or not.", Color.red, Color.white).setToggleState(Configuration.GetConfig().AntiKick);
            new QMToggleButton(this, 2, 0, "Enable\nAnti Block", delegate
            {
                Configuration.GetConfig().AntiBlock = true;
                Configuration.SaveConfiguration();
            }, "Disable\nAnti Block", delegate
            {
                Configuration.GetConfig().AntiBlock = false;
                Configuration.SaveConfiguration();
            }, "Decide whether you want to see people who you've blocked and/or people who have blocked you.", Color.red, Color.white).setToggleState(Configuration.GetConfig().AntiBlock);
            new QMToggleButton(this, 3, 0, "Enable\nPortal Safety", delegate
            {
                Configuration.GetConfig().PortalSafety = true;
                Configuration.SaveConfiguration();
            }, "Disable\nAnti Public Ban", delegate
            {
                Configuration.GetConfig().PortalSafety = false;
                Configuration.SaveConfiguration();
            }, "This feature enables/disables safety for portals, when enabled it asks you if you want to enter any portal, saves you from ip logging portals, etc.", Color.red, Color.white).setToggleState(Configuration.GetConfig().PortalSafety);
            new QMToggleButton(this, 4, 0, "Enable\nVideo Player Safety", delegate
            {
                Configuration.GetConfig().VideoPlayerSafety = true;
                Configuration.SaveConfiguration();
            }, "Disable\nVideo Player Safety", delegate
            {
                Configuration.GetConfig().VideoPlayerSafety = false;
                Configuration.SaveConfiguration();
            }, "This feature, when enabled, protects you from certain urls people try play via video players", Color.red, Color.white).setToggleState(Configuration.GetConfig().VideoPlayerSafety);
            new QMToggleButton(this, 1, 1, "Enable\nModeration Logger", delegate
            {
                Configuration.GetConfig().LogModerations = true;
                Configuration.SaveConfiguration();
            }, "Disable\nModeration Logger", delegate
            {
                Configuration.GetConfig().LogModerations = false;
                Configuration.SaveConfiguration();
            }, "This feature, when enabled, logs all actions of Moderation against you and other players.", Color.red, Color.white).setToggleState(Configuration.GetConfig().LogModerations);
            new QMToggleButton(this, 1, 2, "Enable\nVideo Player Safety", delegate
            {
                Configuration.GetConfig().VideoPlayerSafety = true;
                Configuration.SaveConfiguration();
            }, "Disable\nVideo Player Safety", delegate
            {
                Configuration.GetConfig().VideoPlayerSafety = false;
                Configuration.SaveConfiguration();
            }, "This feature, when enabled, protects you from certain urls people try play via video players", Color.red, Color.white).setToggleState(Configuration.GetConfig().VideoPlayerSafety);
            new QMToggleButton(this, 2, 1, "Enable\nAnti Public Ban", delegate
            {
                Configuration.GetConfig().AntiPublicBan = true;
                Configuration.SaveConfiguration();
            }, "Disable\nAnti Public Ban", delegate
            {
                Configuration.GetConfig().AntiPublicBan = false;
                Configuration.SaveConfiguration();
            }, "This feature, when enabled, allows you to enter any public instance when you're actually public banned.", Color.red, Color.white).setToggleState(Configuration.GetConfig().AntiPublicBan);
            new QMToggleButton(this, 2, 2, "Enable\nAnti World Triggers", delegate
            {
                Configuration.GetConfig().AntiWorldTriggers = true;
                Configuration.SaveConfiguration();
            }, "Disable\nAnti World Triggers", delegate
            {
                Configuration.GetConfig().AntiWorldTriggers = false;
                Configuration.SaveConfiguration();
            }, "This feature, when enabled, allows you to enter any public instance when you're actually public banned.", Color.red, Color.white).setToggleState(Configuration.GetConfig().AntiWorldTriggers);
        }
    }
}
