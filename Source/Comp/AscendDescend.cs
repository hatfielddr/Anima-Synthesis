using RimWorld;
using Verse;

namespace AnimaSynthesis
{
	class AscendDescend : MapComponent
	{
		public AscendDescend(Map m) : base(m) { }

		public void Swap(Thing previousThing, ThingDef def)
		{
			if (def != null)
			{
				IntVec3 pos = previousThing.Position;
				previousThing.DeSpawn();
				AS_Plant plant = (AS_Plant)GenSpawn.Spawn(def, pos, this.map);
				plant.Growth = 0.001f;
			}
            else
            {
				Log.Error("ThingDef not found", true);
            }
        }
	}
}
