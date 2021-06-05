using Verse;
using HarmonyLib;
using RimWorld;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using System.Reflection.Emit;
using System;

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
            var exteriorConditionalFalse = generator.DefineLabel();
            var skipSubplant = generator.DefineLabel();
            var labelCheckNull = generator.DefineLabel();
            var local7 = generator.DeclareLocal(typeof(AS_Plant));
            for (int i = 0; i < code.Count; i++)
            {

                yield return code[i];

                if (i + 3 < code.Count && code[i+3].opcode == OpCodes.Bne_Un_S)
                {
                    //code[i + 2].labels.Add(labelAnimaTree);
                    // if anima tree, jump
                    //yield return new CodeInstruction(OpCodes.Beq_S, labelAnimaTree);
                    // load plant
                    //yield return new CodeInstruction(OpCodes.Ldloc_S, 5);

                    yield return new CodeInstruction(OpCodes.Isinst, typeof(AS_Plant));
                    
                    yield return new CodeInstruction(OpCodes.Brfalse_S, exteriorConditionalFalse);
                    i += 3;
                    code[i + 10].labels.Add(exteriorConditionalFalse);
                    // load the anima sprout def
                    //yield return new CodeInstruction(OpCodes.Ldsfld, typeof(AS_DefOf).GetField("Plant_TreeAnimaSprout"));

                    // return until we get to the next change
                    yield return code[++i];
                    yield return code[++i];
                    yield return code[++i];
                    yield return code[++i];

                    // overwrite the jump label for the subplant 
                    yield return new CodeInstruction(OpCodes.Brfalse_S, skipSubplant);
                    i++;

                    // return until we get to the next change
                    yield return code[++i];
                    yield return code[++i];
                    yield return code[++i];
                    yield return code[++i];

                    code[i + 1].labels.Add(labelCheckNull);
                    var ins = new CodeInstruction(OpCodes.Ldloc_S, 5);
                    ins.labels.Add(skipSubplant);
                    yield return ins;
                    yield return new CodeInstruction(OpCodes.Call, (MethodInfo)typeof(ThingCompUtility).GetMethods(AccessTools.all).FirstOrDefault(m => m.Name == "TryGetComp").MakeGenericMethod(typeof(CompGrowthCooldown)));
                    yield return new CodeInstruction(OpCodes.Stloc_S, local7);
                    yield return new CodeInstruction(OpCodes.Ldloc_S, local7);
                    yield return new CodeInstruction(OpCodes.Brfalse_S, labelCheckNull);
                    yield return new CodeInstruction(OpCodes.Ldloc_S, local7);
                    yield return new CodeInstruction(OpCodes.Callvirt, typeof(CompGrowthCooldown).GetMethod("WarmUp"));
                }
            }
        }
    }
}
