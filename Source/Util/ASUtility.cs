using RimWorld;
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
			var pawn = PawnGenerator.GeneratePawn(new PawnGenerationRequest(pawnKind, Faction.OfPlayer, PawnGenerationContext.NonPlayer, tile, false, false, false, false, false, true, 0f, false, true, false, false, false, false, false, false, 0f, null, 0f, null, null, null, null, new float?(0.2f), 0, null, Gender.Male, null, null, null, null));
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
			transformed.SetFaction(original.Faction);
			transformed.story = original.story;
			transformed.relations = original.relations;
			transformed.skills = original.skills;
			transformed.workSettings = original.workSettings;
			transformed.ThingID = original.ThingID;
			if (ModLister.RoyaltyInstalled && original.royalty != null)
			{
				transformed.royalty = original.royalty;
				foreach (Map map in Find.Maps)
				{
					foreach (Thing thing in map.listerThings.AllThings)
					{
						CompBladelinkWeapon compBladelinkWeapon = thing.TryGetComp<CompBladelinkWeapon>();
						bool flag9 = compBladelinkWeapon != null && compBladelinkWeapon.bondedPawn == original;
						if (flag9)
						{
							compBladelinkWeapon.bondedPawn = transformed;
						}
					}
					transformed.apparel = original.apparel;
					transformed.equipment = original.equipment;
					transformed.inventory = original.inventory;
				}
			}
		}
    }
}
