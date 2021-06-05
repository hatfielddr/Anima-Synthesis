using RimWorld;
using Verse;

namespace AnimaSynthesis
{
    class CompProperties_AdvancedBuildings : CompProperties
    {
        public float radius;
        public TechLevel techLevel;

        public CompProperties_AdvancedBuildings()
        {
            this.compClass = typeof(CompAdvancedBuildings);
        }
    }
}
