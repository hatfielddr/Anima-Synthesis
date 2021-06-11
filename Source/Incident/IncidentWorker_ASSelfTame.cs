using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using Verse;

namespace AnimaSynthesis
{
    class IncidentWorker_ASSelfTame : IncidentWorker
    {

		private IEnumerable<Pawn> Candidates(Map map)
		{
			return from x in map.mapPawns.AllPawnsSpawned
				   where x.RaceProps.Animal && x.Faction == null && !x.Position.Fogged(x.Map) && !x.InMentalState && !x.Downed && x.def.defName == "AnimaSpud"
				   select x;
		}

		private IEnumerable<Thing> Tree(Map map)
        {
			return map.listerThings.AllThings.OfType<AS_Plant>();
        }

		protected override bool CanFireNowSub(IncidentParms parms)
		{
			Map map = (Map)parms.target;
			AS_Plant plant = (AS_Plant)this.Tree(map).FirstOrDefault();
			if (plant == null)
            {
				return false;
            }
			return plant.GrowthRate > 1 && this.Candidates(map).Any<Pawn>();
		}

		protected override bool TryExecuteWorker(IncidentParms parms)
		{
			Map map = (Map)parms.target;
			List<Pawn> pawns = this.Candidates(map).ToList();
			foreach (Pawn pawn in pawns)
			{
				if (pawn == null)
				{
					return false;
				}
				if (pawn.guest != null)
				{
					pawn.guest.SetGuestStatus(null, false);
				}
				string value = pawn.LabelIndefinite();
				bool flag = pawn.Name != null;
				pawn.SetFaction(Faction.OfPlayer, null);
				string str;
				if (!flag && pawn.Name != null)
				{
					if (pawn.Name.Numerical)
					{
						str = "LetterAnimalSelfTameAndNameNumerical".Translate(value, pawn.Name.ToStringFull, pawn.Named("ANIMAL")).CapitalizeFirst();
					}
					else
					{
						str = "LetterAnimalSelfTameAndName".Translate(value, pawn.Name.ToStringFull, pawn.Named("ANIMAL")).CapitalizeFirst();
					}
				}
				else
				{
					str = "LetterAnimalSelfTame".Translate(pawn).CapitalizeFirst();
				}
				base.SendStandardLetter("LetterLabelAnimalSelfTame".Translate(pawn.KindLabel, pawn).CapitalizeFirst(), str, LetterDefOf.PositiveEvent, parms, pawn, Array.Empty<NamedArgument>());
			}
			return true;
		}
	}
}
