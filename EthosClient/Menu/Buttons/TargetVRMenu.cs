using EthosClient.Utils;
using EthosClient.Wrappers;
using RubyButtonAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace EthosClient.Menu
{
    public class TargetVRMenu : QMNestedButton
    {
        public TargetVRMenu(EthosVRButton config) : base(config.Menu, config.X, config.Y, config.Name, config.Tooltip, GeneralUtils.GetColor(config.ColorScheme.Colors[0]), GeneralUtils.GetColor(config.ColorScheme.Colors[1]), GeneralUtils.GetColor(config.ColorScheme.Colors[2]), GeneralUtils.GetColor(config.ColorScheme.Colors[3]))
        {
            new QMSingleButton(this, 1, 0, "Teleport", new Action(() =>
            {
                PlayerWrappers.GetCurrentPlayer().transform.position = PlayerWrappers.GetSelectedPlayer(GeneralWrappers.GetQuickMenu()).transform.position;
            }), "Teleports you to the selected player.", Color.red, Color.white);

            new QMToggleButton(this, 2, 0, "Local\nBlock", delegate
            {
                PlayerWrappers.GetSelectedPlayer(GeneralWrappers.GetQuickMenu()).prop_VRCAvatarManager_0.gameObject.SetActive(false);
            }, "Local\nUnblock", delegate
            {
                PlayerWrappers.GetSelectedPlayer(GeneralWrappers.GetQuickMenu()).prop_VRCAvatarManager_0.gameObject.SetActive(true);
            }, "Decide whether you want to block this user locally, meaning, the blocking doesn't effect them but it also makes them disappear to yourself.", Color.red, Color.white);
        }
    }
}
