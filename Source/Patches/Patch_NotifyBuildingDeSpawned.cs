using RimWorld;
using Verse;
using HarmonyLib;

namespace AnimaSynthesis
{
    [HarmonyPatch(typeof(Building), "Destroy")]
    static class Patch_NotifyBuildingDeSpawned
    {
        static void Postfix(ref Building __instance)
        {
            if (__instance.Spawned)
                __instance.Map.GetComponent<CheckBuildings>().Notify_BuildingChange();
        }
    }
}
