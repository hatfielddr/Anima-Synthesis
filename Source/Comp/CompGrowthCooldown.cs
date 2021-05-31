using RimWorld;
using Verse;

namespace AnimaSynthesis
{
    public class CompGrowthCooldown : ThingComp
    {
        public CompProperties_GrowthCooldown Props => (CompProperties_GrowthCooldown)this.props;

        float meditationCooldown = 30000;

        public override void CompTickRare()
        {
            base.CompTickRare();
            meditationCooldown = meditationCooldown-250 > 0 ? (meditationCooldown - 250) : 0;
        }

        public void ResetCooldown()
        {
            Log.Message("Should Reset");
            meditationCooldown = Props.ticksBeforeCooldown;
        }

        public float GetMeditationCooldown()
        {
            return meditationCooldown;
        }
    }
}
