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
                        array[i].transform.Find("SelectRegion").GetComponent<Renderer>().material.SetColor("_Color", Color.red);
                        array[i].transform.Find("SelectRegion").GetComponent<Renderer>().sharedMaterial.SetColor("_Color", Color.magenta);
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
                        array[i].transform.Find("SelectRegion").GetComponent<Renderer>().material.SetColor("_Color", Color.red);
                        array[i].transform.Find("SelectRegion").GetComponent<Renderer>().sharedMaterial.SetColor("_Color", Color.magenta);
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
            new QMToggleButton(this, 1, 1, "Infinite\nJump", delegate
            {
                if (VRCInputManager.Method_Public_Static_ObjectPublicStSiBoSiObBoSiObStSiUnique_String_0("Jump").prop_Boolean_0)
                {
                    VRCPlayer.field_Internal_Static_VRCPlayer_0.GetComponent<VRCMotionState>().field_Private_Boolean_0 = true;
                }
            }, "Finite\nJump", delegate 
            {
                if (VRCInputManager.Method_Public_Static_ObjectPublicStSiBoSiObBoSiObStSiUnique_String_0("Jump").prop_Boolean_0)
                {
                    VRCPlayer.field_Internal_Static_VRCPlayer_0.GetComponent<VRCMotionState>().field_Private_Boolean_0 = false;
                }
            }, "Enable/Disable Infinite jumping, when this is enabled, it allows you to jump as much as possible >lol", Color.red, Color.white);
        }
    }
}
