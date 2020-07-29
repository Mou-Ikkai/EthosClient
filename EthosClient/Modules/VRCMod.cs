using EthosClient.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VRC;

namespace EthosClient.Modules
{
    public class VRCMod
    {
        public virtual string Name => "Example Module";

        public virtual string Description => "Example Description";

        public virtual string Author => "Example Author";

        public VRCMod() => ConsoleUtil.Info($"VRC Mod {this.Name} has Loaded made by {Author}. {this.Description}");

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

        public virtual void OnPlayerJoin(Player player)
        {

        }

        public virtual void OnPlayerLeft(Player player)
        {

        }
    }
}
