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
        public TargetVRMenu(EthosVRButton config) : base(config.Menu, config.X, config.Y, config.Name, config.Tooltip, config.ColorScheme[0], config.ColorScheme[1], config.ColorScheme[2], config.ColorScheme[3])
        {
            new QMSingleButton(this, 1, 0, "Teleport", new Action(() =>
            {
                PlayerWrappers.GetCurrentPlayer().transform.position = PlayerWrappers.GetSelectedPlayer(GeneralWrappers.GetQuickMenu()).transform.position;
            }), "Teleports you to the selected player.", Color.red, Color.white);
            new QMToggleButton(this, 2, 0, "Local\nBlock", delegate
            {
                var player = PlayerWrappers.GetSelectedPlayer(GeneralWrappers.GetQuickMenu());
                player.gameObject.SetActive(false);
            }, "Local\nUnblock", delegate
            {
                var player = PlayerWrappers.GetSelectedPlayer(GeneralWrappers.GetQuickMenu());
                player.gameObject.SetActive(true);
            }, "Decide whether you want to block this user locally, meaning, the blocking doesn't effect them but it also makes them disappear to yourself.", Color.red, Color.white);
        }
    }
}
