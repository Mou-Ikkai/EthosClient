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
    public class ButtonRepositionVRMenu : QMNestedButton
    {
        public ButtonRepositionVRMenu() : base("UIElementsMenu", 1, 1, "Reposition\nButtons", "Welcome to the repositioning menu, place your buttons wherever it suits you.", Color.red, Color.white, Color.red, Color.cyan)
        {

        }
    }
}
