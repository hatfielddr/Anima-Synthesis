using HarmonyLib;
using RimWorld;
using Verse;
using AnimaSynthesis;

internal static class Patch_AnimaGen
{
    [HarmonyPatch(typeof(GenStep_AnimaTrees), "TrySpawnAt")]
    internal class GenStep_AnimaSprout
    {
        static bool Prefix(ref bool __result, IntVec3 cell, Map map, out Thing plant)
        {
            cell.GetPlant(map)?.Destroy();
            plant = GenSpawn.Spawn(AS_DefOf.Plant_TreeAnimaSprout, cell, map);
            ((Plant)plant).Growth = .001f;
            __result = (plant != null);
            return false;
        }
    }

    
}