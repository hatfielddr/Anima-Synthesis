using Verse;
using HarmonyLib;
using RimWorld;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace AnimaSynthesis
{
    [HarmonyPatch]
    internal class Patch_MeditateJob
    {
        static MethodBase TargetMethod()
        {
            return typeof(JobDriver_Meditate)
                .GetMethods(AccessTools.all)
                .FirstOrDefault(m => m.Name == "<MakeNewToils>b__15_3");
        }

        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
        {
            var code = instructions.ToList();
            var labelAnimaSynthesis = generator.DefineLabel();
            for (int i = 0; i < code.Count; i++)
            {

                yield return code[i];

                if (i + 1 < code.Count && code[i+1].opcode == OpCodes.Bne_Un_S)
                {
                    Log.Message("triggered");
                    code[i + 2].labels.Add(labelAnimaSynthesis);
                    // if anima tree, jump
                    yield return new CodeInstruction(OpCodes.Beq_S, labelAnimaSynthesis);
                    // load plant
                    yield return new CodeInstruction(OpCodes.Ldloc_S, 5);
                    // and its def
                    yield return new CodeInstruction(OpCodes.Ldfld, typeof(Verse.Thing).GetField("def"));
                    //load the anima sprout def
                    yield return new CodeInstruction(OpCodes.Ldsfld, typeof(AS_DefOf).GetField("Plant_TreeAnimaSprout"));
                }
            }
        }
    }
}
