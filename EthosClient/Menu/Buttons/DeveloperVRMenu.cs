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
    public class DeveloperVRMenu : QMNestedButton
    {
        public DeveloperVRMenu(EthosVRButton config) : base(config.Menu, config.X, config.Y, config.Name, config.Tooltip, config.ColorScheme[0], config.ColorScheme[1], config.ColorScheme[2], config.ColorScheme[3])
        {
            new QMToggleButton(this, 1, 0, "Go\nAutistic", new Action(() =>
            {
                GeneralUtils.Autism = true;
            }), "Revert\nYour Autism", new Action(() =>
            {
                GeneralUtils.Autism = false;
            }), "Do some crazy shit idk", Color.red, Color.white).setToggleState(GeneralUtils.Autism);
        }
    }
}
