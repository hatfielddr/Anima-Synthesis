using RimWorld;
using Verse;
using Verse.Sound;

namespace AnimaSynthesis
{
	class AscendDescend : MapComponent
	{
		public AscendDescend(Map m) : base(m) { }

		public void Ascend(Thing previousThing, ThingDef def)
		{
			if (def != null)
			{
				IntVec3 pos = previousThing.Position;
				previousThing.DeSpawn();
				AS_Plant plant = (AS_Plant)GenSpawn.Spawn(def, pos, this.map);
				plant.Growth = 0.001f;
				// TODO - Add a new visual effect for 1.3
				SoundDefOf.MechClusterDefeated.PlayOneShotOnCamera();
			}
            else
            {
				Log.Error("ThingDef not found", true);
            }
        }

		public void Descend(Thing previousThing, ThingDef def)
		{
			if (def != null)
			{
				IntVec3 pos = previousThing.Position;
				previousThing.DeSpawn();
				AS_Plant plant = (AS_Plant)GenSpawn.Spawn(def, pos, this.map);
				plant.Growth = 0.001f;
				// TODO - Same as prev
			}
			else
			{
				Log.Error("ThingDef not found", true);
			}
		}
	}
}
