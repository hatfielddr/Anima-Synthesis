using RimWorld;
using Verse;
using HarmonyLib;

namespace AnimaSynthesis
{
    [HarmonyPatch(typeof(Building), "SpawnSetup")]
    static class Patch_NotifyBuildingSpawned
    {
        static void Postfix(ref Building __instance)
        {
            __instance.Map.GetComponent<CheckBuildings>().Notify_BuildingChange(__instance);
        }
    }
}
