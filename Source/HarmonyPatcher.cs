using HarmonyLib;
using Verse;
using RimWorld;

namespace AnimaSynthesis
{
    [StaticConstructorOnStartup]
    public static class HarmonyPatcher
    {
        static HarmonyPatcher()
        {
            Harmony val = new Harmony("rimworld.animasynthesis");
            val.PatchAll();
        }
    }
}
