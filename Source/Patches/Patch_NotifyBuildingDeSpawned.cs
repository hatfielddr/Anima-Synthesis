using RimWorld;
using Verse;
using HarmonyLib;

namespace AnimaSynthesis
{
    [HarmonyPatch(typeof(Building), "DeSpawn")]
    static class Patch_NotifyBuildingDeSpawned
    {
        static void Postfix(ref Building __instance)
        {
            __instance.Map.GetComponent<CheckBuildings>().Notify_BuildingChange(__instance);
        }
    }
}
