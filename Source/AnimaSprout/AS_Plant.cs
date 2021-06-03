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
                float focusOffset = this.TryGetComp<CompMeditationFocus>().GetStatOffset();
                float meditationCooldown = this.TryGetComp<CompGrowthCooldown>().GetMeditationCooldown();
                CompProperties_GrowthCooldown myProps = (CompProperties_GrowthCooldown)this.GetComp<CompGrowthCooldown>().props;

                if (focusOffset == 0)
                {
                    growthOffset = 1;
                }
                else
                {
                    growthOffset = 0;
                }

                return base.GrowthRate * growthOffset * (float)(meditationCooldown / (myProps.ticksBeforeCooldown / 2));
            }
        }
    }
}
