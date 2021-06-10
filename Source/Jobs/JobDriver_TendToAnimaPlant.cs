using RimWorld;
using System.Collections.Generic;
using Verse;
using Verse.AI;

namespace AnimaSynthesis
{
    class JobDriver_TendToAnimaPlant : JobDriver
    {

        protected AS_Plant AS_Plant
        {
            get
            {
                return (AS_Plant)this.job.GetTarget(TargetIndex.A).Thing;
            }
        }

        public override bool TryMakePreToilReservations(bool errorOnFailed)
        {
            Pawn pawn = this.pawn;
            LocalTargetInfo target = this.AS_Plant;
            Job job = this.job;
            return pawn.Reserve(target, job, 1, -1, null, errorOnFailed);
        }

        protected override IEnumerable<Toil> MakeNewToils()
        {
            this.FailOnDespawnedNullOrForbidden(TargetIndex.A);
            this.FailOnBurningImmobile(TargetIndex.A);
            yield return Toils_Goto.GotoThing(TargetIndex.A, PathEndMode.Touch);
            yield return Toils_General.Wait(500, TargetIndex.None).FailOnDestroyedNullOrForbidden(TargetIndex.A).FailOnCannotTouch(TargetIndex.A, PathEndMode.Touch).FailOn(() => !this.AS_Plant.needTend).WithProgressBarToilDelay(TargetIndex.A, false, -0.5f);
            yield return new Toil
            {
                initAction = delegate ()
                {
                    int skill = pawn.skills.skills.Find((SkillRecord r) => r.def.defName == "Plants").levelInt / 2;
                    if (Rand.RangeInclusive(0, 11 - skill) <= 5)
                    {
                        this.AS_Plant.tickBeforeTend += 120000;
                    }
                    else
                    {
                        MoteMaker.ThrowText(pawn.DrawPos, pawn.Map, "Tending failed", 5f);
                        pawn.jobs.StartJob(new Job(AS_DefOf.AnimaSynthesis_TendToAnimaPlant, TargetA));
                    }
                    pawn.skills.skills.Find((SkillRecord r) => r.def.defName == "Plants").Learn(20, false);
                },
                defaultCompleteMode = ToilCompleteMode.Instant
            };
            yield break;
        }
    }
}
