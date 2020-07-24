using FuneralClientV2.Settings;
using FuneralClientV2.Utils;
using FuneralClientV2.Wrappers;
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

namespace FuneralClientV2.Menu
{
    public class DeveloperVRMenu : QMNestedButton
    {
        public DeveloperVRMenu() : base("UIElementsMenu", 3, 0, "Developer\nOnly", "Just some experimental features I guess", Color.red, Color.white, Color.red, Color.cyan)
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
