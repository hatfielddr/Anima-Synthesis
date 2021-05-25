using HarmonyLib;
using RimWorld;
using Verse;

[StaticConstructorOnStartup]
internal static class Patch_AnimaRespawn
{
    [HarmonyPatch(typeof(IncidentWorker), "CanFireNow")]
    internal class IncidentWorker_CanFireNow
    {
        static bool Prefix(ref IncidentWorker __instance)
        {
            if (__instance.def.defName.EqualsIgnoreCase("AnimaTreeSpawn"))
            {
                return false;
            }
            return true;
        }
    }
}