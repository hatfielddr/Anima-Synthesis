using RimWorld;
using System.Collections.Generic;
using Verse;
using Verse.AI;

namespace AnimaSynthesis
{
    class JobDriver_AscendPlant : JobDriver
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
            yield return Toils_General.Wait(1000, TargetIndex.None).FailOnDestroyedNullOrForbidden(TargetIndex.A).FailOnCannotTouch(TargetIndex.A, PathEndMode.Touch).FailOn(() => this.AS_Plant.LifeStage != PlantLifeStage.Mature).WithProgressBarToilDelay(TargetIndex.A, false, -0.5f);
            yield return new Toil
            {
                initAction = delegate ()
                {
                    this.AS_Plant.Map.GetComponent<AscendDescend>().Ascend(this.AS_Plant, this.AS_Plant.TryGetComp<CompAscendDescend>().GetHigherRank());
                    pawn.skills.skills.Find((SkillRecord r) => r.def.defName == "Plants").Learn(200, false);
                },
                defaultCompleteMode = ToilCompleteMode.Instant
            };
            yield break;
        }
    }
}
