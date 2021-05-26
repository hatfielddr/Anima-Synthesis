using RimWorld;
using System.Collections.Generic;
using System.Text;
using Verse;

namespace AnimaSynthesis
{
    public class AnimaSprout : Plant
    {
        public int plantProgress;
        int duration = 420000;
        public int tickBeforeTend = 120000;
        float progressPercentage;
        private List<IntVec3> cellsAround = new List<IntVec3>();

        public bool needTend
        {
            get
            {
                return tickBeforeTend <= 0;
            }
        }
        private int EstimatedTicksLeft
        {
            get
            {
                return this.duration - this.plantProgress;
            }
        }

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look<int>(ref this.plantProgress, "plantProgress", 0, false);
            Scribe_Values.Look<int>(ref this.tickBeforeTend, "tickBeforeTend", 0, false);
        }

        public override void TickRare()
        {
            base.TickRare();
            progressPercentage = (float)this.plantProgress / this.duration;
            this.tickBeforeTend -= 250;
            if (!this.needTend)
            {
                this.plantProgress += 250;
            }
        }

        private void Reset()
        {
            this.plantProgress = 0;
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

        public override string GetInspectString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(base.GetInspectString());
            if (stringBuilder.Length != 0)
            {
                stringBuilder.AppendLine();
            }
            stringBuilder.AppendLine("FermentationProgress".Translate(this.progressPercentage.ToStringPercent(), this.EstimatedTicksLeft.ToStringTicksToPeriod()));
            if (this.needTend)
            {
                stringBuilder.AppendLine("ANeedTend".Translate());
            }
            else
            {
                stringBuilder.AppendLine("ANeedTIn".Translate(this.tickBeforeTend.ToStringTicksToPeriod()));
            }
            return stringBuilder.ToString().TrimEndNewlines();
        }

        public List<IntVec3> CellsAroundA(IntVec3 pos, Map map)
        {
            List<IntVec3> cellsAround = new List<IntVec3>();
            if (!pos.InBounds(map))
            {
                return cellsAround;
            }
            IEnumerable<IntVec3> cells = CellRect.CenteredOn(this.Position, 5).Cells;
            foreach (IntVec3 item in cells)
            {
                if (item.InHorDistOf(pos, 4.9f))
                {
                    cellsAround.Add(item);
                }
            }
            return cellsAround;
        }

        public override IEnumerable<Gizmo> GetGizmos()
        {
            foreach (Gizmo g in base.GetGizmos())
            {
                yield return g;
            }
            if (Prefs.DevMode)
            {
                yield return new Command_Action
                {
                    defaultLabel = "Debug: Set progress to max",
                    action = delegate ()
                    {
                        this.plantProgress = this.duration;
                    }
                };
                yield return new Command_Action
                {
                    defaultLabel = "Debug: Add progress",
                    action = delegate ()
                    {
                        this.plantProgress += 12000;
                        this.tickBeforeTend -= 12000;
                    }
                };
            }
            yield break;
        }
    }
}