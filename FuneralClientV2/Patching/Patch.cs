using Harmony;
using Il2CppSystem.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MethodBase = System.Reflection.MethodBase;

namespace FuneralClientV2.Patching
{
    public class Patch
    {
        public static Dictionary<string, HarmonyInstance> PatchIDS = new Dictionary<string, HarmonyInstance>();
        public string ID { get; set; }

        public MethodBase TargetMethod { get; set; }

        public HarmonyMethod Prefix { get; set; }

        public HarmonyMethod Postfix { get; set; }

        public Patch(string Identifier, MethodBase Target, HarmonyMethod Before, HarmonyMethod After)
        {
            ID = Identifier;
            TargetMethod = Target;
            Prefix = Before;
            Postfix = After;
        }

        public void ApplyPatch()
        {
            if (PatchIDS.ContainsKey(ID))
                PatchIDS[ID].Patch(TargetMethod, Prefix, Postfix); else
            {
                PatchIDS.Add(ID, HarmonyInstance.Create(ID));
                PatchIDS[ID].Patch(TargetMethod, Prefix, Postfix);
            }
        }
    }
}
