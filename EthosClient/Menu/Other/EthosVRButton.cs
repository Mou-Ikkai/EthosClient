using RubyButtonAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace EthosClient.Menu
{
    public class EthosVRButton
    {
        public string Menu { get; set; }

        public string ID { get; set; }

        public string Name { get; set; }

        public string Tooltip { get; set; }

        public int X { get; set; }

        public int Y { get; set; }

        public EthosColorScheme ColorScheme { get; set; }

        public bool AllowReposition { get; set; }

        public EthosVRButton(string id, string menu, string name, string tooltip, int x, int y, EthosColorScheme scheme, bool allowreposition)
        {
            ID = id;
            Menu = menu;
            Name = name;
            Tooltip = tooltip;
            X = x;
            Y = y;
            ColorScheme = scheme;
            AllowReposition = allowreposition;
        }
    }
}
