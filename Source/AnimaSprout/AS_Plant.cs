using RimWorld;
using System.Text;
using Verse;

namespace AnimaSynthesis
{
    public class AS_Plant : Plant
    {
        public bool techOffset = false;
        public float meditationFactor;

        public override float GrowthRate
        {
            get
            {
                CompProperties_GrowthCooldown myProps = (CompProperties_GrowthCooldown)this.GetComp<CompGrowthCooldown>().props;
                techOffset = this.TryGetComp<CompAdvancedBuildings>().CheckBuildings();
                float meditationCooldown = this.TryGetComp<CompGrowthCooldown>().GetMeditationCooldown();
                meditationFactor = (float)(meditationCooldown / (myProps.ticksBeforeCooldown / 2));
                float techFactor = techOffset ? 0 : (float)1;

                return base.GrowthRate * techFactor * meditationFactor;
            }
        }

        public override string GetInspectString()
        {
            CompProperties_GrowthCooldown myProps = (CompProperties_GrowthCooldown)this.GetComp<CompGrowthCooldown>().props;
            StringBuilder stringBuilder = new StringBuilder(base.GetInspectString());
            stringBuilder.AppendLine();
            if (meditationFactor < 1f)
            {
                stringBuilder.AppendLine("MeditationNeed".Translate(this.LabelShort));
            }
            return stringBuilder.ToString().TrimEndNewlines();
        }
    }
}
