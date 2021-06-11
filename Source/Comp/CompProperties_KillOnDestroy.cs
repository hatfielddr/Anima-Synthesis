using RimWorld;
using Verse;

namespace AnimaSynthesis
{
    class CompProperties_KillOnDestroy : CompProperties
    {
        public ThingDef thing;
        public string message;

        public CompProperties_KillOnDestroy()
        {
            this.compClass = typeof(CompKillOnDestroy);
        }
    }
}
