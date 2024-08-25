using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices;

namespace LetHeirsReign_NativeModloader
{
    public static class Patches
    {
        public static IEnumerable<CodeInstruction> fitToRule_Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
        {
            var codes = new List<CodeInstruction>(instructions);

            Label label = generator.DefineLabel();

            for (int i = 0; i < codes.Count; i++)
            {
                if (codes[i].Is(OpCodes.Callvirt, AccessTools.Method(typeof(Actor), "isUnitOk")))
                {
                    Console.WriteLine(MethodBase.GetCurrentMethod().Name + ": FOUNDED 1");

                    codes[i + 1] = new CodeInstruction(OpCodes.Brtrue_S, label);
                }

                if (codes[i].Is(OpCodes.Callvirt, AccessTools.Method(typeof(ActorBase), "isKing")))
                {
                    Console.WriteLine(MethodBase.GetCurrentMethod().Name + ": FOUNDED 2");

                    codes[i - 1].labels.Add(label);
                }

                else
                {
                    Console.WriteLine(MethodBase.GetCurrentMethod().Name + ": UNFOUNDED");
                }
            }

            return codes.AsEnumerable();
        }
    }
}
