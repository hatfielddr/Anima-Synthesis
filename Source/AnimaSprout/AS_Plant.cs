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
                
                if (focusOffset == 0)
                {
                    growthOffset = 1;
                }
                else
                {
                    growthOffset = 0;
                }

                return base.GrowthRate * growthOffset * (float)(meditationCooldown / 60000);
            }
        }
    }
}
