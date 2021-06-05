using RimWorld;
using Verse;

namespace AnimaSynthesis
{
    class AS_Plant : Plant
    {
        public override float GrowthRate
        {
            get
            {
                float growthOffset;
                bool techOffset = false; 
                techOffset = this.TryGetComp<CompAdvancedBuildings>().CheckBuildings();
                float meditationCooldown = this.TryGetComp<CompGrowthCooldown>().GetMeditationCooldown();
                CompProperties_GrowthCooldown myProps = (CompProperties_GrowthCooldown)this.GetComp<CompGrowthCooldown>().props;

                if (techOffset)
                {
                    growthOffset = 0;
                }
                else
                {
                    growthOffset = 1;
                }

                return base.GrowthRate * growthOffset * (float)(meditationCooldown / (myProps.ticksBeforeCooldown / 2));
            }
        }
    }
}
