using EthosClient.Settings;
using EthosClient.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace EthosClient.Menu
{
    public class EthosColorScheme
    {
        public List<EthosColor> Colors = new List<EthosColor>();

        public EthosColorScheme(Color btnBackgroundColor, Color btnTextColor, Color backbtnBackgroundColor, Color backbtnTextColor)
        {
            Colors.Add(GeneralUtils.GetEthosColor(btnBackgroundColor));
            Colors.Add(GeneralUtils.GetEthosColor(btnTextColor));
            Colors.Add(GeneralUtils.GetEthosColor(backbtnBackgroundColor));
            Colors.Add(GeneralUtils.GetEthosColor(backbtnTextColor));
        }
        //more stuff to be added ok
    }
}
