using RimWorld;
using Verse;

namespace AnimaSynthesis
{
    public class CompGrowthCooldown : ThingComp
    {
        public CompProperties_GrowthCooldown Props => (CompProperties_GrowthCooldown)this.props;
        float meditationCooldown = 0;

        public override void PostExposeData()
        {
            base.PostExposeData();
            Scribe_Values.Look<float>(ref this.meditationCooldown, "meditationCooldown", 0, false);
        }

        public override void CompTickLong()
        {
            base.CompTickLong();
            meditationCooldown = meditationCooldown-2000 > 0 ? (meditationCooldown - 2000) : 0;
        }

        public void ResetCooldown()
        {
            meditationCooldown = Props.ticksBeforeCooldown;
        }

        public void WarmUp()
        {
            meditationCooldown = meditationCooldown + 4 < Props.ticksBeforeCooldown ? (meditationCooldown + 4) : Props.ticksBeforeCooldown;
        }

        public float GetMeditationCooldown()
        {
            return meditationCooldown;
        }
    }
}
