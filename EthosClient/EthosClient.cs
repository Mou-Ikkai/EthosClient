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
        #region Support for Side UI and button collision ._.
        private bool AvatarDeleteMode = false;
        private int AvatarX = 0, AvatarY = 1;
        private static PageAvatar PAviSaved = new PageAvatar() { avatar = new SimpleAvatarPedestal() };
        #endregion
        public override void VRChat_OnUiManagerInit()
        {
            #region Adding Main Menu Buttons
            if (!Configuration.GetConfig().SideUI) new MainMenu(GeneralUtils.GetEthosVRButton("MainMenu"));
            #endregion
            #region Creating Side UI Menu
            if (Configuration.GetConfig().SideUI)
            {
                Transform background = QuickMenu.prop_QuickMenu_0.transform.Find("QuickMenu_NewElements/_Background/Panel");

                Transform sidemenu = UnityEngine.Object.Instantiate(background, QuickMenu.prop_QuickMenu_0.transform.Find("QuickMenu_NewElements/_Background"));

                GameObject UserInterface = GameObject.Find("/UserInterface/MenuContent");

                QuickMenu.prop_QuickMenu_0.GetComponent<BoxCollider>().size += QuickMenu.prop_QuickMenu_0.GetComponent<BoxCollider>().size;

                sidemenu.GetComponent<RectTransform>().anchoredPosition -= new Vector2(sidemenu.GetComponent<RectTransform>().anchoredPosition.x * 12, -410);
                sidemenu.transform.localScale = new Vector3(sidemenu.transform.localScale.x / 2, sidemenu.transform.localScale.y, sidemenu.transform.localScale.z);

                QMNestedButton exploitMenu = new QMNestedButton("ShortcutMenu", 0, 0, "Exploits", "Exploit Stuff");
                QMNestedButton favMenu = new QMNestedButton("ShortcutMenu", 1, 0, "Extended Favourite", "Avatars n stuff");
                QMNestedButton moveMenu = new QMNestedButton("ShortcutMenu", 2, 0, "Movement", "Fly Speed and other Stuff");
                QMNestedButton settings = new QMNestedButton("ShortcutMenu", 3, 0, "Settings", "Settings");
                QMNestedButton credits = new QMNestedButton("ShortcutMenu", 4, 0, "Credits", "Who did this");

                favMenu.getMainButton().getGameObject().GetComponent<RectTransform>().anchoredPosition = exploitMenu.getMainButton().getGameObject().GetComponent<RectTransform>().anchoredPosition;
                moveMenu.getMainButton().getGameObject().GetComponent<RectTransform>().anchoredPosition = exploitMenu.getMainButton().getGameObject().GetComponent<RectTransform>().anchoredPosition;
                settings.getMainButton().getGameObject().GetComponent<RectTransform>().anchoredPosition = exploitMenu.getMainButton().getGameObject().GetComponent<RectTransform>().anchoredPosition;
                credits.getMainButton().getGameObject().GetComponent<RectTransform>().anchoredPosition = exploitMenu.getMainButton().getGameObject().GetComponent<RectTransform>().anchoredPosition;

                exploitMenu.getMainButton().getGameObject().GetComponent<RectTransform>().sizeDelta /= new Vector2(0.75f, 2.5f);
                favMenu.getMainButton().getGameObject().GetComponent<RectTransform>().sizeDelta /= new Vector2(0.75f, 2.5f);
                moveMenu.getMainButton().getGameObject().GetComponent<RectTransform>().sizeDelta /= new Vector2(0.75f, 2.5f);
                settings.getMainButton().getGameObject().GetComponent<RectTransform>().sizeDelta /= new Vector2(0.75f, 2.5f);
                credits.getMainButton().getGameObject().GetComponent<RectTransform>().sizeDelta /= new Vector2(0.75f, 2.5f);

                exploitMenu.getMainButton().getGameObject().GetComponent<RectTransform>().anchoredPosition += Vector2.down * (-420);
                favMenu.getMainButton().getGameObject().GetComponent<RectTransform>().anchoredPosition += Vector2.down * (-210);
                moveMenu.getMainButton().getGameObject().GetComponent<RectTransform>().anchoredPosition += Vector2.down * (420 * (0f));
                settings.getMainButton().getGameObject().GetComponent<RectTransform>().anchoredPosition += Vector2.down * (420 * (0.5f));
                credits.getMainButton().getGameObject().GetComponent<RectTransform>().anchoredPosition += Vector2.down * (420 * (1f));

                //MelonCoroutines.Start(LoadLogo());

                exploitMenu.getMainButton().getGameObject().GetComponent<RectTransform>().anchoredPosition -= Vector2.right * (420 * (0.625f));
                favMenu.getMainButton().getGameObject().GetComponent<RectTransform>().anchoredPosition -= Vector2.right * (420 * (0.625f));
                moveMenu.getMainButton().getGameObject().GetComponent<RectTransform>().anchoredPosition -= Vector2.right * (420 * (0.625f));
                settings.getMainButton().getGameObject().GetComponent<RectTransform>().anchoredPosition -= Vector2.right * (420 * (0.625f));
                credits.getMainButton().getGameObject().GetComponent<RectTransform>().anchoredPosition -= Vector2.right * (420 * (0.625f));

                exploitMenu.getMainButton().getGameObject().transform.parent = sidemenu;
                favMenu.getMainButton().getGameObject().transform.parent = sidemenu;
                moveMenu.getMainButton().getGameObject().transform.parent = sidemenu;
                settings.getMainButton().getGameObject().transform.parent = sidemenu;
                credits.getMainButton().getGameObject().transform.parent = sidemenu;

                QMSingleButton AvatarByID = new QMSingleButton(exploitMenu, 1, 0, "Avatar\nBy\nID", delegate
                {
                    ConsoleUtil.Info("Enter Avatar ID: ");
                    string ID = System.Console.ReadLine();
                    VRC.Core.API.SendRequest($"avatars/{ID}", VRC.Core.BestHTTP.HTTPMethods.Get, new ApiModelContainer<ApiAvatar>(), null, true, true, 3600f, 2, null);
                    new PageAvatar
                    {
                        avatar = new SimpleAvatarPedestal
                        {
                            field_Internal_ApiAvatar_0 = new ApiAvatar
                            {
                                id = ID
                            }
                        }
                    }.ChangeToSelectedAvatar();
                    GeneralWrappers.GetVRCUiPopupManager().AlertPopup("<color=cyan>Success!</color>", "<color=green>Successfully cloned that avatar by It's Avatar ID.</color>");
                }, "Sets your current avatar using an avatar ID.", Color.red, Color.white);

                QMSingleButton JoinByID = new QMSingleButton(exploitMenu, 2, 0, "Join\nBy\nID", delegate
                {
                    ConsoleUtil.Info("Enter Instance ID: ");
                    string ID = System.Console.ReadLine();
                    Networking.GoToRoom(ID);
                }, "Joins an instance by It's ID.", Color.red, Color.white);

                QMToggleButton wrldTrigger = new QMToggleButton(exploitMenu, 3, 0, "Enable\nWorld Triggers", delegate
                {
                    GeneralUtils.WorldTriggers = true;
                }, "Disable\nWorld Triggers", delegate
                {
                    GeneralUtils.WorldTriggers = false;
                }, "Decide whether you want other people to see your interactions with \"local\" triggers.", Color.red, Color.white);
                wrldTrigger.setToggleState(GeneralUtils.WorldTriggers);

                QMToggleButton froceClone = new QMToggleButton(exploitMenu, 4, 0, "Enable\nForce Clone", delegate
                {
                    GeneralUtils.ForceClone = true;
                }, "Disable\nForce Clone", delegate
                {
                    GeneralUtils.ForceClone = false;
                }, "(EXPERIMENTAL) Enable/Disable Force Clone, when this is enabled, any avatar can be cloned apart from private ones.", Color.red, Color.white);
                froceClone.setToggleState(GeneralUtils.ForceClone);

                QMSingleButton NextAV = new QMSingleButton(favMenu, 0, 0, "Next", delegate
                {
                    //to-do
                }, "Go to the next page", Color.red, Color.white);

                QMSingleButton PrevAV = new QMSingleButton(favMenu, 0, 1, "Back", delegate
                {
                    //to-do
                }, "Go back to the previous page", Color.red, Color.white);

                QMSingleButton AddAV = new QMSingleButton(favMenu, 5, 0, "Add\nCurrent Avatar", delegate
                {
                    var currentAvatar = PlayerWrappers.GetCurrentPlayer().GetAPIAvatar();
                    Configuration.GetConfig().ExtendedFavoritedAvatars.Add(new FavoritedAvatar(currentAvatar.name, currentAvatar.id, currentAvatar.authorName, currentAvatar.authorId));
                    Configuration.SaveConfiguration();
                    GeneralWrappers.GetVRCUiPopupManager().AlertPopup("<color=cyan>Success!</color>", "<color=green>Successfully added your current Avatar to extended favorites</color>");
                }, "Adds your current avatar to the extended favorites list", Color.red, Color.white);

                QMSingleButton RemoveAV = new QMSingleButton(favMenu, 5, 1, "Remove\nCurrent Avatar", delegate
                {
                    var currentAvatar = GeneralUtils.GetExtendedFavorite(PlayerWrappers.GetCurrentPlayer().GetAPIAvatar().id);
                    Configuration.GetConfig().ExtendedFavoritedAvatars.Remove(currentAvatar);
                    Configuration.SaveConfiguration();
                    GeneralWrappers.GetVRCUiPopupManager().AlertPopup("<color=cyan>Success!</color>", "<color=green>Successfully removed your current Avatar from extended favorites</color>");
                }, "Removes your current avatar from the extended favorites list", Color.red, Color.white);

                QMToggleButton DeletMode = new QMToggleButton(favMenu, 5, -1, "Delete\nMode", delegate
                {
                    AvatarDeleteMode = true;
                }, "Normal\nMode", delegate
                {
                    AvatarDeleteMode = false;
                }, "Enable/Disable Delete Mode, with this on, you can remove avatars from this list by just clicking their respective buttons", Color.red, Color.white);

                foreach (var avatar in Configuration.GetConfig().ExtendedFavoritedAvatars)
                {
                    if (AvatarX == 4)
                    {
                        if (AvatarY != 4)
                        {
                            new QMSingleButton(favMenu, AvatarX, AvatarY, avatar.Name, delegate
                            {
                                if (AvatarDeleteMode)
                                {
                                    Configuration.GetConfig().ExtendedFavoritedAvatars.Remove(avatar);
                                    Configuration.SaveConfiguration();
                                    GeneralWrappers.GetVRCUiPopupManager().AlertPopup("<color=cyan>Success!</color>", "<color=green>Successfully removed this Avatar from extended favorites</color>");
                                }
                                else
                                {
                                    new ApiAvatar() { id = avatar.ID }.Get(new Action<ApiContainer>(x =>
                                    {
                                        PAviSaved.avatar.field_Internal_ApiAvatar_0 = x.Model.Cast<ApiAvatar>(); // can fix better later.
                                        PAviSaved.ChangeToSelectedAvatar();
                                    }), null, null, false);
                                }
                            }, $"by {avatar.Author}\nSwitch into this avatar.", Color.red, Color.white);
                        }
                        AvatarY++;
                    }
                    else
                    {
                        new QMSingleButton(favMenu, AvatarX, AvatarY, avatar.Name, delegate
                        {
                            if (AvatarDeleteMode)
                            {
                                Configuration.GetConfig().ExtendedFavoritedAvatars.Remove(avatar);
                                Configuration.SaveConfiguration();
                                GeneralWrappers.GetVRCUiPopupManager().AlertPopup("<color=cyan>Success!</color>", "<color=green>Successfully removed this Avatar from extended favorites</color>");
                            }
                            else
                            {
                                new ApiAvatar() { id = avatar.ID }.Get(new Action<ApiContainer>(x =>
                                {
                                    PAviSaved.avatar.field_Internal_ApiAvatar_0 = x.Model.Cast<ApiAvatar>(); // can fix better later.
                                    PAviSaved.ChangeToSelectedAvatar();
                                }), null, null, false);
                            }
                        }, $"by {avatar.Author}\nSwitch into this avatar.", Color.red, Color.white);
                        AvatarX++;
                    }
                }

                QMToggleButton fly = new QMToggleButton(moveMenu, 1, 0, "Enable\nFlight", delegate
                {
                    Physics.gravity = Vector3.zero;
                    GeneralUtils.Flight = true;
                    GeneralUtils.ToggleColliders(!GeneralUtils.Flight);
                }, "Disable\nFlight", delegate
                {
                    Physics.gravity = GeneralUtils.SavedGravity;
                    GeneralUtils.Flight = false;
                    GeneralUtils.ToggleColliders(!GeneralUtils.Flight);
                }, "Toggle Flight and move around within the air with ease!", Color.red, Color.white);
                fly.setToggleState(GeneralUtils.Flight);

                QMToggleButton spinBot = new QMToggleButton(moveMenu, 2, 0, "Enable\nSpin Bot", delegate
                {
                    GeneralUtils.SpinBot = true;
                }, "Disable\nSpin Bot", delegate
                {
                    GeneralUtils.SpinBot = false;
                }, "Toggle the spin bot and go zooming in circles lol", Color.red, Color.white);
                spinBot.setToggleState(GeneralUtils.SpinBot);

                QMToggleButton antiKick = new QMToggleButton(settings, 1, 0, "Enable\nAnti Kick", delegate
                {
                    Configuration.GetConfig().AntiKick = true;
                    Configuration.SaveConfiguration();
                }, "Disable\nAnti Kick", delegate
                {
                    Configuration.GetConfig().AntiKick = false;
                    Configuration.SaveConfiguration();
                }, "Harvest the infinite power of this world and decide whether people can kick you from the instance or not.", Color.red, Color.white);
                antiKick.setToggleState(Configuration.GetConfig().AntiKick);
                QMToggleButton antiBlock = new QMToggleButton(settings, 2, 0, "Enable\nAnti Block", delegate
                {
                    Configuration.GetConfig().AntiBlock = true;
                    Configuration.SaveConfiguration();
                }, "Disable\nAnti Block", delegate
                {
                    Configuration.GetConfig().AntiBlock = false;
                    Configuration.SaveConfiguration();
                }, "Decide whether you want to see people who you've blocked and/or people who have blocked you.", Color.red, Color.white);
                antiBlock.setToggleState(Configuration.GetConfig().AntiBlock);
                QMToggleButton portalSafty = new QMToggleButton(settings, 3, 0, "Enable\nPortal Safety", delegate
                {
                    Configuration.GetConfig().PortalSafety = true;
                    Configuration.SaveConfiguration();
                }, "Disable\nPortal Safety", delegate
                {
                    Configuration.GetConfig().PortalSafety = false;
                    Configuration.SaveConfiguration();
                }, "This feature enables/disables safety for portals, when enabled it asks you if you want to enter any portal, saves you from ip logging portals, etc.", Color.red, Color.white);
                portalSafty.setToggleState(Configuration.GetConfig().PortalSafety);
                QMToggleButton vidSafty = new QMToggleButton(settings, 4, 0, "Enable\nVideo Player Safety", delegate
                {
                    Configuration.GetConfig().VideoPlayerSafety = true;
                    Configuration.SaveConfiguration();
                }, "Disable\nVideo Player Safety", delegate
                {
                    Configuration.GetConfig().VideoPlayerSafety = false;
                    Configuration.SaveConfiguration();
                }, "This feature, when enabled, protects you from certain urls people try play via video players", Color.red, Color.white);
                vidSafty.setToggleState(Configuration.GetConfig().VideoPlayerSafety);
                QMToggleButton modLog = new QMToggleButton(settings, 1, 1, "Enable\nModeration Logger", delegate
                {
                    Configuration.GetConfig().LogModerations = true;
                    Configuration.SaveConfiguration();
                }, "Disable\nModeration Logger", delegate
                {
                    Configuration.GetConfig().LogModerations = false;
                    Configuration.SaveConfiguration();
                }, "This feature, when enabled, logs all actions of Moderation against you and other players.", Color.red, Color.white);
                modLog.setToggleState(Configuration.GetConfig().LogModerations);
                QMToggleButton antiPubBan = new QMToggleButton(settings, 1, 2, "Enable\nAnti Public Ban", delegate
                {
                    Configuration.GetConfig().AntiPublicBan = true;
                    Configuration.SaveConfiguration();
                }, "Disable\nAnti Public Ban", delegate
                {
                    Configuration.GetConfig().AntiPublicBan = false;
                    Configuration.SaveConfiguration();
                }, "This feature, when enabled, allows you to enter any public instance when you're actually public banned.", Color.red, Color.white);
                antiPubBan.setToggleState(Configuration.GetConfig().AntiPublicBan);
                QMToggleButton antiwrldTrigger = new QMToggleButton(settings, 2, 1, "Enable\nAnti World Triggers", delegate
                {
                    Configuration.GetConfig().AntiWorldTriggers = true;
                    Configuration.SaveConfiguration();
                }, "Disable\nAnti World Triggers", delegate
                {
                    Configuration.GetConfig().AntiWorldTriggers = false;
                    Configuration.SaveConfiguration();
                }, "This feature, when enabled, prevents other people from using world triggers.", Color.red, Color.white);
                antiwrldTrigger.setToggleState(Configuration.GetConfig().AntiWorldTriggers);

                new QMSingleButton(credits, 1, 0, "GitHub", new Action(() =>
                {
                    Process.Start("https://github.com/Yaekith/Funeral_ClientV2");
                }), "Open the github repository in a new browser window", Color.red, Color.white);
                new QMSingleButton(credits, 2, 0, "Discord", new Action(() =>
                {
                    Process.Start("https://discord.gg/8fwurVW");
                }), "Join the official discord", Color.red, Color.white);
                new QMSingleButton(credits, 3, 0, "Daily\nNotice", new Action(() =>
                {
                    new System.Threading.Thread(() =>
                    {
                        var information = new WebClient().DownloadString("https://pastebin.com/raw/BjsgVdQp");
                        GeneralUtils.InformHudText(Color.cyan, information);
                    })
                    { IsBackground = true }.Start();
                }), "Gather information about the latest notice in the Discord", Color.red, Color.white);
                new QMSingleButton(credits, 4, 0, "Credits", new Action(() =>
                {
                    GeneralUtils.InformHudText(Color.yellow, "Yaekith - Developer\n404 - Developer");
                }), "Displays who made this cheat", Color.red, Color.white);
            }
            

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
            HttpClient client = new HttpClient();
            var response = client.GetStringAsync("http://yaekiths-projects.xyz/special.txt").Result;
            foreach(var line in response.Split('\n')) GeneralUtils.Authorities.Add(line.Split(':')[0], line.Split(':')[1]);
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

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e) => ConsoleUtil.Exception(e.ExceptionObject as Exception);

        public override void OnUpdate() 
        {
            #region On Module Updates
            for (int i = 0; i < GeneralUtils.Modules.Count; i++)
                GeneralUtils.Modules[i].OnUpdate();
            #endregion
        }
    }
}
