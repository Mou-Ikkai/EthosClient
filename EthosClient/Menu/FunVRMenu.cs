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
        public FunVRMenu(QMNestedButton parent) : base(parent, 2, 1, "Fun", "A menu full of fun stuff!", Color.red, Color.white, Color.red, Color.cyan)
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
            new QMToggleButton(this, 3, 0, "Enable\nCustom Serialisation", delegate
            {
                GeneralUtils.DontSerialise = true;
            }, "Disable\nCustom Serialisation", delegate
            {
                GeneralUtils.DontSerialise = false;
            }, "(EXPERIMENTAL) Enable/Disable Custom PhotonView Serialisation", Color.red, Color.white).setToggleState(GeneralUtils.WorldTriggers);
        }
    }
}
