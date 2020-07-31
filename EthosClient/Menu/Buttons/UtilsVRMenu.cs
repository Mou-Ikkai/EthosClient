using BestHTTP;
using EthosClient.Menu;
using EthosClient.Settings;
using EthosClient.Wrappers;
using RubyButtonAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using VRC;
using VRC.Core;
using VRC.SDKBase;
using VRC.UI;
using VRCSDK2;

namespace EthosClient.Utils
{
    public class UtilsVRMenu : QMNestedButton
    {
        public UtilsVRMenu(QMNestedButton parent, EthosVRButton config) : base(parent, config.X, config.Y, config.Name, config.Tooltip, GeneralUtils.GetColor(config.ColorScheme.Colors[0]), GeneralUtils.GetColor(config.ColorScheme.Colors[1]), GeneralUtils.GetColor(config.ColorScheme.Colors[2]), GeneralUtils.GetColor(config.ColorScheme.Colors[3]))
        {
            new QMToggleButton(this, 1, 0, "Enable\nFlight", delegate
            {
                Physics.gravity = Vector3.zero;
                GeneralUtils.Flight = true;
                GeneralUtils.ToggleColliders(!GeneralUtils.Flight);
            }, "Disable\nFlight", delegate
            {
                Physics.gravity = GeneralUtils.SavedGravity;
                GeneralUtils.Flight = false;
                GeneralUtils.ToggleColliders(!GeneralUtils.Flight);
            }, "Toggle Flight and move around within the air with ease!", Color.red, Color.white).setToggleState(GeneralUtils.Flight);

            new QMToggleButton(this, 2, 0, "Enable\nESP", delegate
            {
                GeneralUtils.ESP = true;
                GameObject[] array = GameObject.FindGameObjectsWithTag("Player");
                for (int i = 0; i < array.Length; i++)
                {
                    if (array[i].transform.Find("SelectRegion"))
                    {
                        array[i].transform.Find("SelectRegion").GetComponent<Renderer>().material.color = Color.green;
                        array[i].transform.Find("SelectRegion").GetComponent<Renderer>().sharedMaterial.color = Color.red;
                        GeneralWrappers.GetHighlightsFX().EnableOutline(array[i].transform.Find("SelectRegion").GetComponent<Renderer>(), GeneralUtils.ESP);
                    }
                }
            }, "Disable\nESP", delegate
            {
                GeneralUtils.ESP = false;
                GameObject[] array = GameObject.FindGameObjectsWithTag("Player");
                for (int i = 0; i < array.Length; i++)
                {
                    if (array[i].transform.Find("SelectRegion"))
                    {
                        array[i].transform.Find("SelectRegion").GetComponent<Renderer>().material.color = Color.green;
                        array[i].transform.Find("SelectRegion").GetComponent<Renderer>().sharedMaterial.color = Color.red;
                        GeneralWrappers.GetHighlightsFX().EnableOutline(array[i].transform.Find("SelectRegion").GetComponent<Renderer>(), GeneralUtils.ESP);
                    }
                }
            }, "Decide whether you want the upper game, get an advantage, and see all players anywhere within the world.", Color.red, Color.white).setToggleState(GeneralUtils.ESP);

            new QMSingleButton(this, 3, 0, "Avatar\nBy\nID", delegate
            {
                ConsoleUtil.Info("Enter Avatar ID: ");
                string ID = Console.ReadLine();
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

            new QMSingleButton(this, 4, 0, "Join\nBy\nID", delegate
            {
                ConsoleUtil.Info("Enter Instance ID: ");
                string ID = Console.ReadLine();
                Networking.GoToRoom(ID);
            }, "Joins an instance by It's ID.", Color.red, Color.white);

            new QMToggleButton(this, 1, 1, "Enable\nCustom Serialization", delegate
            {
                GeneralUtils.CustomSerialization = true;
            }, "Disable\nCustom Serialization", delegate
            {
                GeneralUtils.CustomSerialization = false;
            }, "Enable/Disables custom serialization on your photon view, meaning with this enabled, no one can see you move around.", Color.red, Color.white).setToggleState(GeneralUtils.CustomSerialization);

            new QMToggleButton(this, 2, 1, "Can't Hear\non Non Friends", delegate
            {
                GeneralUtils.CantHearOnNonFriends = true;
                foreach (var player in GeneralWrappers.GetPlayerManager().GetAllPlayers())
                {
                    if (!player.GetAPIUser().isFriend)
                    {
                        player.GetVRCPlayer().field_Internal_Boolean_3 = false;
                    }
                }
            }, "Can Hear\non Non Friends", delegate
            {
                GeneralUtils.CantHearOnNonFriends = false;
                foreach (var player in GeneralWrappers.GetPlayerManager().GetAllPlayers())
                {
                    if (!player.GetAPIUser().isFriend)
                    {
                        player.GetVRCPlayer().field_Internal_Boolean_3 = true;
                    }
                }
            }, "Decide whether you want your friends to only hear you in game or not.", Color.red, Color.white).setToggleState(GeneralUtils.CantHearOnNonFriends);
        }
    }
}
