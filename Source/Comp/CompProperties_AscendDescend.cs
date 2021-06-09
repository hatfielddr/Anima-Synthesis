using RimWorld;
using Verse;

namespace AnimaSynthesis
{
    class CompProperties_AscendDescend : CompProperties
    {
        public ThingDef lowerRank = null;
        public ThingDef higherRank = null;

        public CompProperties_AscendDescend()
        {
            this.compClass = typeof(CompAscendDescend);
        }
    }
}
