using JetBrains.Annotations;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using Verse;

namespace AnimaSynthesis
{
    class ASUtility
    {

        public static Pawn GenerateASPawn_Transform(PawnKindDef pawnKind, Pawn original, int tile)
        {
            var transformed = PawnGenerator.GeneratePawn(new PawnGenerationRequest(pawnKind, Faction.OfPlayer));
            TransferEverything(original, transformed);

            return transformed;
        }

        public static Pawn GenerateASPawn_New(PawnKindDef pawnKind, int tile)
        {
			var pawn = PawnGenerator.GeneratePawn(new PawnGenerationRequest());
			return pawn;
		}

        public static void TransferEverything(Pawn original, Pawn transformed)
        {
			transformed.Name = original.Name;
			bool flag = original.playerSettings != null;
			if (flag)
			{
				transformed.playerSettings.hostilityResponse = original.playerSettings.hostilityResponse;
				transformed.playerSettings.AreaRestriction = original.playerSettings.AreaRestriction;
				transformed.playerSettings.medCare = original.playerSettings.medCare;
				transformed.playerSettings.selfTend = original.playerSettings.selfTend;
			}
			if (original.ageTracker != null)
			{
				transformed.ageTracker.AgeChronologicalTicks = original.ageTracker.AgeChronologicalTicks;
			}
			transformed.foodRestriction = original.foodRestriction;
			transformed.outfits = original.outfits;
			transformed.drugs = original.drugs;
			transformed.timetable = original.timetable;
			transformed.needs = original.needs;
			transformed.health = original.health;
			transformed.story = original.story;
			// TEMPORARY UNTIL MORE GENDERS ARE IMPLEMENTED
			// ==========
			transformed.story.bodyType = BodyTypeDefOf.Male;
			transformed.gender = Gender.Male;
			// ==========
			TransferRelations(original, transformed);

			transformed.skills = original.skills;
			transformed.workSettings = original.workSettings;
			transformed.ThingID = original.ThingID;

			transformed.apparel.DestroyAll();
			transformed.equipment.DestroyAllEquipment();
			transformed.inventory.DestroyAll();

			if (ModLister.RoyaltyInstalled && original.royalty != null)
			{
				transformed.royalty = original.royalty;
				foreach (Map map in Find.Maps)
				{
					foreach (Thing thing in map.listerThings.AllThings)
					{
						CompBladelinkWeapon compBladelinkWeapon = thing.TryGetComp<CompBladelinkWeapon>();
						bool flag9 = compBladelinkWeapon != null && compBladelinkWeapon.CodedPawn == original;
						if (flag9)
						{
							compBladelinkWeapon.CodeFor(transformed);
						}
					}
                }
			}
		}

		public static void TransferRelations([NotNull] Pawn pawn1, [NotNull] Pawn pawn2,
											 Predicate<PawnRelationDef> predicate = null)
		{
			if (pawn1.relations == null) return;
			List<DirectPawnRelation> enumerator = pawn1.relations.DirectRelations.MakeSafe().ToList();
			predicate = predicate ?? (r => true); //if no filter is set, have it pass everything 
			foreach (DirectPawnRelation directPawnRelation in enumerator.Where(d => predicate(d.def)))
			{
				if (directPawnRelation.def.implied) continue;
				pawn1.relations?.RemoveDirectRelation(directPawnRelation); //make sure we remove the relations first 
				pawn2.relations?.AddDirectRelation(directPawnRelation.def,
												   directPawnRelation.otherPawn); //TODO restrict these to special relationships? 
			}

			foreach (Pawn pRelatedPawns in pawn1.relations.PotentiallyRelatedPawns.ToList()
			) //make copies so we don't  invalidate the enumerator mid way through 
				foreach (PawnRelationDef pawnRelationDef in pRelatedPawns.GetRelations(pawn1).Where(d => predicate(d)).ToList())
				{
					if (pawnRelationDef.implied) continue;
					pRelatedPawns.relations.RemoveDirectRelation(pawnRelationDef, pawn1);
					pRelatedPawns.relations.AddDirectRelation(pawnRelationDef, pawn2);
				}
		}
	}

	public static class Util
    {
		[NotNull]
		public static IEnumerable<T> MakeSafe<T>([CanBeNull, NoEnumeration] this IEnumerable<T> enumerable)
		{
			return enumerable ?? Enumerable.Empty<T>();
		}
	}
}
