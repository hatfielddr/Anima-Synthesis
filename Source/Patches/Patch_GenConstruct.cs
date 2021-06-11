using RimWorld;
using Verse;
using HarmonyLib;
using Verse.AI;

namespace AnimaSynthesis
{
    [HarmonyPatch(typeof(WorkGiver_ConstructFinishFrames), "JobOnThing")]
    static class Patch_GenConstruct
    {
        static void Postfix(ref Job __result, Pawn pawn, Thing t)
        {
            if (t.def.defName.Equals("Frame_AnimaCropCircle"))
            {
                if (pawn.skills.GetSkill(SkillDefOf.Plants).Level < 12)
                {
                    __result = null;
                }
                else
                {
                    // Skill is high enough
                    return;
                }
            }
        }
    }
}
