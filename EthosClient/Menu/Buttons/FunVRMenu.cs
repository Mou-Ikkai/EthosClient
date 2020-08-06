using EthosClient.Menu;
using EthosClient.Menu.Buttons;
using EthosClient.Wrappers;
using RubyButtonAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Video;
using VRC;
using VRC.SDKBase;

namespace EthosClient.Utils
{
    public class FunVRMenu : QMNestedButton
    {
        public FunVRMenu(QMNestedButton parent, EthosVRButton config) : base(parent, config.X, config.Y, config.Name, config.Tooltip, GeneralUtils.GetColor(config.ColorScheme.Colors[0]), GeneralUtils.GetColor(config.ColorScheme.Colors[1]), GeneralUtils.GetColor(config.ColorScheme.Colors[2]), GeneralUtils.GetColor(config.ColorScheme.Colors[3]))
        {
            new QMToggleButton(this, 1, 0, "Enable\nWorld Triggers", delegate
            {
                GeneralUtils.WorldTriggers = true;
            }, "Disable\nWorld Triggers", delegate
            {
                GeneralUtils.WorldTriggers = false;
            }, "Decide whether you want other people to see your interactions with \"local\" triggers.", Color.red, Color.white).setToggleState(GeneralUtils.WorldTriggers);

            new QMToggleButton(this, 2, 0, "Enable\nForce Clone", delegate
            {
                GeneralUtils.ForceClone = true;
            }, "Disable\nForce Clone", delegate
            {
                GeneralUtils.ForceClone = false;
            }, "(EXPERIMENTAL) Enable/Disable Force Clone, when this is enabled, any avatar can be cloned apart from private ones.", Color.red, Color.white).setToggleState(GeneralUtils.ForceClone);

            new QMSingleButton(this, 3, 0, "Interact with\nAll Triggers", delegate
            {
                foreach (VRC_Trigger trigger in Resources.FindObjectsOfTypeAll<VRC_Trigger>())
                {
                    if (!trigger.name.Contains("Avatar") && !trigger.name.Contains("Chair"))
                    {
                        trigger.Interact();
                    }
                }
            }, "Interact with all triggers in the world.", Color.red, Color.white);

            new QMSingleButton(this, 4, 0, "Interact with\nAll Mirrors", delegate
            {
                foreach (VRC_Trigger trigger in Resources.FindObjectsOfTypeAll<VRC_Trigger>())
                {
                    if (trigger.name.Contains("Mirror"))
                    {
                        trigger.Interact();
                    }
                }
            }, "Interact with all mirrors in the world.", Color.red, Color.white);

            new SelfControlVRMenu(this);

            new QMSingleButton(this, 1, 2, "Play\nVideo on Player", delegate
            {
                var videoPlayers = Resources.FindObjectsOfTypeAll<VRC_SyncVideoPlayer>();

                if (videoPlayers.Count() > 0)
                    Networking.RPC(RPC.Destination.All, videoPlayers[0].gameObject, "Play", new Il2CppSystem.Object[0]);

            }, "Plays the current video on the video player", Color.red, Color.white);

            new QMSingleButton(this, 0, 0, "Pause\nVideo on Player", delegate
            {
                var videoPlayers = Resources.FindObjectsOfTypeAll<VRC_SyncVideoPlayer>();

                if (videoPlayers.Count() > 0)
                    Networking.RPC(RPC.Destination.All, videoPlayers[0].gameObject, "Pause", new Il2CppSystem.Object[0]);

            }, "Stops the current video on the video player", Color.red, Color.white);

            new QMSingleButton(this, 2, 1, "Stop\nVideo on Player", delegate
            {
                var videoPlayers = Resources.FindObjectsOfTypeAll<VRC_SyncVideoPlayer>();

                if (videoPlayers.Count() > 0)
                    Networking.RPC(RPC.Destination.All, videoPlayers[0].gameObject, "Stop", new Il2CppSystem.Object[0]);

            }, "Pauses the current video on the video player", Color.red, Color.white);

            new QMSingleButton(this, 2, 2, "Next\nVideo on Player", delegate
            {
                var videoPlayers = Resources.FindObjectsOfTypeAll<VRC_SyncVideoPlayer>();

                if (videoPlayers.Count() > 0)
                    Networking.RPC(RPC.Destination.All, videoPlayers[0].gameObject, "Next", new Il2CppSystem.Object[0]);

            }, "Skips the current video on the video player", Color.red, Color.white);

            new QMSingleButton(this, 0, 1, "Previous\nVideo on Player", delegate
            {
                var videoPlayers = Resources.FindObjectsOfTypeAll<VRC_SyncVideoPlayer>();

                if (videoPlayers.Count() > 0)
                    Networking.RPC(RPC.Destination.All, videoPlayers[0].gameObject, "Previous", new Il2CppSystem.Object[0]);

            }, "Rewinds the current video on the video player", Color.red, Color.white);

            new QMSingleButton(this, 3, 1, "Clear\nVideos on Player", delegate
            {
                var videoPlayers = Resources.FindObjectsOfTypeAll<VRC_SyncVideoPlayer>();

                if (videoPlayers.Count() > 0)
                    Networking.RPC(RPC.Destination.All, videoPlayers[0].gameObject, "Clear", new Il2CppSystem.Object[0]);

            }, "Clears the video in queue on the video player", Color.red, Color.white);

            new QMSingleButton(this, 3, 2, "Speed up\nVideo on Player", delegate
            {
                var videoPlayers = Resources.FindObjectsOfTypeAll<VRC_SyncVideoPlayer>();

                if (videoPlayers.Count() > 0)
                    Networking.RPC(RPC.Destination.All, videoPlayers[0].gameObject, "SpeedUp", new Il2CppSystem.Object[0]);

            }, "Speeds up the current video on the video player", Color.red, Color.white);

            new QMSingleButton(this, 2, 3, "Slow down\nVideo on Player", delegate
            {
                var videoPlayers = Resources.FindObjectsOfTypeAll<VRC_SyncVideoPlayer>();

                if (videoPlayers.Count() > 0)
                    Networking.RPC(RPC.Destination.All, videoPlayers[0].gameObject, "SpeedDown", new Il2CppSystem.Object[0]);

            }, "Slows down the current video on the video player", Color.red, Color.white);

            new QMSingleButton(this, 4, 1, "Add\nVideo to Player", delegate
            {
                ConsoleUtil.Info("Enter URL: ");
                string url = Console.ReadLine();
                var videoPlayers = Resources.FindObjectsOfTypeAll<VRC_SyncVideoPlayer>();

                if (videoPlayers.Count() > 0)
                {
                    Networking.RPC(RPC.Destination.All, videoPlayers[0].gameObject, "AddURL", new Il2CppSystem.Object[]
                    {
                        (Il2CppSystem.String)url
                    });
                }

            }, "Adds a video to the queue on the video player", Color.red, Color.white);
        }
    }
}
