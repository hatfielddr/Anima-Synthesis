using RimWorld;
using System.Collections.Generic;
using System.Linq;
using Verse;

namespace AnimaSynthesis
{
    class CompKillOnDestroy : ThingComp
    {
        public CompProperties_KillOnDestroy Props => (CompProperties_KillOnDestroy)this.props;

		private IEnumerable<Pawn> Candidates(Map map)
		{
			return from x in map.mapPawns.AllPawnsSpawned
				   where x.def.defName == "AnimaSpud"
				   select x;
		}

		public override void PostDestroy(DestroyMode mode, Map previousMap)
        {
			if (previousMap != null)
			{
				List<Pawn> pawns = this.Candidates(previousMap).ToList();
				if (!this.Props.message.NullOrEmpty())
				{
					Messages.Message(this.Props.message, new TargetInfo(this.parent.Position, previousMap, false), MessageTypeDefOf.NegativeEvent, true);
				}
				foreach (Pawn pawn in pawns)
				{
					if (pawn.def == this.Props.thing)
                    {
						pawn.Kill(null, null);
                    }
				}
			}
		}
    }
}
