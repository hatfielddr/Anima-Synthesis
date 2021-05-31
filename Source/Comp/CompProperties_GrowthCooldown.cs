using RimWorld;
using Verse;

namespace AnimaSynthesis
{
    public class CompProperties_GrowthCooldown : CompProperties
    {
        public float ticksBeforeCooldown;

        public CompProperties_GrowthCooldown()
        {
            this.compClass = typeof(CompGrowthCooldown);
        }
    }
}
