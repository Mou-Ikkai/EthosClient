using EthosClient.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EthosClient.Modules
{
    public class VRCMod
    {
        public virtual string Name => "Example Module";

        public virtual string Description => "Example Description";

        public VRCMod() => ConsoleUtil.Info($"VRC Mod {this.Name} has Loaded. {this.Description}");

        public virtual bool RequiresUpdate => false;

        public virtual void OnStart()
        {

        }
        public virtual void OnAppQuit()
        {

        }
        public virtual void OnUiLoad()
        {

        }

        public virtual void OnUpdate()
        {

        }
    }
}
