using RimWorld;
using System.Collections.Generic;
using System.Text;
using Verse;

namespace AnimaSynthesis
{
    public class AS_Plant : Plant
    {
        public bool techOffset = false;
        public float meditationFactor;
        public int tickBeforeTend = 120000;

        public bool needTend
        {
            get
            {
                return tickBeforeTend <= 0;
            }
        }

        public override float GrowthRate
        {
            get
            {
                CompProperties_GrowthCooldown myProps = (CompProperties_GrowthCooldown)this.GetComp<CompGrowthCooldown>().props;
                techOffset = this.TryGetComp<CompAdvancedBuildings>().CheckBuildings();
                float meditationCooldown = this.TryGetComp<CompGrowthCooldown>().GetMeditationCooldown();
                meditationFactor = (float)(meditationCooldown / (myProps.ticksBeforeCooldown / 2));
                float techFactor = techOffset ? 0 : (float)1;

                float tendFactor = needTend ? 0 : (float)1;

                return base.GrowthRate * techFactor * meditationFactor * tendFactor;
            }
        }

        public override void TickLong()
        {
            base.TickLong();
            this.tickBeforeTend -= 2000;
        }

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look<int>(ref this.tickBeforeTend, "tickBeforeTend", 0, false);
        }

        public override string GetInspectString()
        {
            CompProperties_GrowthCooldown myProps = (CompProperties_GrowthCooldown)this.GetComp<CompGrowthCooldown>().props;
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(base.GetInspectString());
            if (stringBuilder.Length != 0)
            {
                stringBuilder.AppendLine();
            }
            if (this.needTend)
            {
                stringBuilder.AppendLine("NeedsTending".Translate(this.LabelShort));
            }
            else
            {
                stringBuilder.AppendLine("NeedsTendIn".Translate(this.LabelShort, this.tickBeforeTend.ToStringTicksToPeriod()));
            }
            if (meditationFactor < 1f)
            {
                stringBuilder.AppendLine("MeditationNeed".Translate(this.LabelShort));
            }
            return stringBuilder.ToString().TrimEndNewlines();
        }

        public void ResetTend(Pawn pawn)
        {
            int skill = pawn.skills.skills.Find((SkillRecord r) => r.def.defName == "Plants").levelInt;
            System.Random rnd = new System.Random();
            if (rnd.Next(0, 21 - skill) == 1)
            {
                this.tickBeforeTend = 120000;
            }
            pawn.skills.skills.Find((SkillRecord r) => r.def.defName == "Plants").Learn(100, false);
        }

        public override IEnumerable<Gizmo> GetGizmos()
        {
            CompProperties_AscendDescend Props = (CompProperties_AscendDescend)this.GetComp<CompAscendDescend>().props;
            foreach (Gizmo g in base.GetGizmos())
            {
                yield return g;
            }
            if (Prefs.DevMode)
            {
                yield return new Command_Action
                {
                    defaultLabel = "Debug: Tend to plant",
                    action = delegate ()
                    {
                        this.tickBeforeTend = 120000;
                    }
                };
                yield return new Command_Action
                {
                    defaultLabel = "Debug: Ascend plant",
                    action = delegate ()
                    {
                        this.Map.GetComponent<AscendDescend>().Swap(this, Props.higherRank);
                    }
                };
                yield return new Command_Action
                {
                    defaultLabel = "Debug: Descend plant",
                    action = delegate ()
                    {
                        this.Map.GetComponent<AscendDescend>().Swap(this, Props.lowerRank);
                    }
                };
                yield return new Command_Action
                {
                    defaultLabel = "Debug: Meditate at plant",
                    action = delegate ()
                    {
                        this.TryGetComp<CompGrowthCooldown>().ResetCooldown();
                    }
                };
                yield return new Command_Action
                {
                    defaultLabel = "Debug: Add progress",
                    action = delegate ()
                    {
                        this.tickBeforeTend -= 12000;
                    }
                };
            }
            yield break;
        }
    }
}
