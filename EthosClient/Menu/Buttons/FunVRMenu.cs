using EthosClient.Menu;
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
    public class FunVRMenu : QMNestedButton
    {
        public FunVRMenu(QMNestedButton parent, EthosVRButton config) : base(parent, config.X, config.Y, config.Name, config.Tooltip, GeneralUtils.GetColor(config.ColorScheme.Colors[0]), GeneralUtils.GetColor(config.ColorScheme.Colors[1]), GeneralUtils.GetColor(config.ColorScheme.Colors[2]), GeneralUtils.GetColor(config.ColorScheme.Colors[3]))
        {
            new QMToggleButton(this, 1, 0, "Enable\nSpin Bot", delegate
            {
                GeneralUtils.SpinBot = true;
            }, "Disable\nSpin Bot", delegate
            {
                GeneralUtils.SpinBot = false;
            }, "Toggle the spin bot and go zooming in circles lol", Color.red, Color.white).setToggleState(GeneralUtils.SpinBot);
            new QMToggleButton(this, 2, 0, "Enable\nWorld Triggers", delegate
            {
                GeneralUtils.WorldTriggers = true;
            }, "Disable\nWorld Triggers", delegate
            {
                GeneralUtils.WorldTriggers = false;
            }, "Decide whether you want other people to see your interactions with \"local\" triggers.", Color.red, Color.white).setToggleState(GeneralUtils.WorldTriggers);
            new QMToggleButton(this, 3, 0, "Enable\nForce Clone", delegate
            {
                GeneralUtils.ForceClone = true;
            }, "Disable\nForce Clone", delegate
            {
                GeneralUtils.ForceClone = false;
            }, "(EXPERIMENTAL) Enable/Disable Force Clone, when this is enabled, any avatar can be cloned apart from private ones.", Color.red, Color.white).setToggleState(GeneralUtils.ForceClone);
        }
    }
}
