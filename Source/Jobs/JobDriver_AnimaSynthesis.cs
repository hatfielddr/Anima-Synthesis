using RimWorld;
using System;
using System.Collections.Generic;
using Verse;
using Verse.AI;

namespace AnimaSynthesis
{
    class JobDriver_AnimaSynthesis : JobDriver
    {
        public override bool TryMakePreToilReservations(bool errorOnFailed)
        {
            return pawn.Reserve(this.job.targetA, this.job, 1, -1, null, errorOnFailed);
        }

        protected override IEnumerable<Toil> MakeNewToils()
        {
            this.FailOnDespawnedOrNull(TargetIndex.A);
            yield return Toils_Goto.GotoThing(TargetIndex.A, PathEndMode.InteractionCell);
            Toil toil = Toils_General.Wait(500, TargetIndex.None);
            toil.FailOnCannotTouch(TargetIndex.A, PathEndMode.InteractionCell);
            toil.WithProgressBarToilDelay(TargetIndex.A, false, -0.5f);
            yield return toil;
            Toil transform = new Toil();
			transform.initAction = delegate ()
			{
				Pawn actor = transform.actor;
				AS_Plant plant = (AS_Plant)actor.CurJob.targetA.Thing;
				Action action = delegate ()
				{
					Pawn pawn = ASUtility.GenerateASPawn_Transform(AS_DefOf.SynthesisColonist, actor, plant.Tile);
                    _ = GenSpawn.Spawn(pawn, actor.Position, actor.Map, Rot4.South) as Pawn;
                    actor.DeSpawn(DestroyMode.Vanish);
				};
				action();
			};
			transform.defaultCompleteMode = ToilCompleteMode.Instant;
			yield return transform;
			yield break;
		}
    }
}
