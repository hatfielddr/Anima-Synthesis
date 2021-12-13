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
                    actor.equipment.DropAllEquipment(actor.Position);
                    actor.apparel.DropAll(actor.Position);
                    actor.inventory.DropAllNearPawn(actor.Position);
					Pawn pawn = ASUtility.GenerateASPawn_Transform(AS_DefOf.SynthesisColonist, actor, actor.Tile);
                    Pawn output = GenSpawn.Spawn(pawn, actor.Position, Find.CurrentMap, actor.Rotation) as Pawn;
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
