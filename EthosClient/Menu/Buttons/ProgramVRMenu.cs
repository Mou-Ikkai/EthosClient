using RubyButtonAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace EthosClient.Menu.Buttons
{
    public class ProgramVRMenu : QMNestedButton
    {
        public ProgramVRMenu(QMNestedButton parent) : base(parent, 1, 1, "Programs", "Easily control programs on your computer and launch some.", Color.red, Color.white, Color.red, Color.white)
        {
        }
    }
}
