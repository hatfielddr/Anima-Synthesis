using RimWorld;
using Verse;
using System.Collections.Generic;

namespace AnimaSynthesis
{
    class CheckBuildings : MapComponent
    {
		public CheckBuildings(Map m) : base(m) { }
		public bool dirty;

		public void Notify_BuildingChange()
		{
            /*var plant = ThingMaker.MakeThing(AS_DefOf.Plant_TreeAnimaSprout);
            if (CheckBuildings.CountsAsTechBuilding(b))
            {
                foreach (Thing thing in GenRadial.RadialDistinctThingsAround(b.Position, this.map, plant.TryGetComp<CompAdvancedBuildings>().Props.radius, false))
                {
                    if (thing.GetType().Equals(typeof(AS_Plant)))
                    {
                        this.techBuildingsPerCell.Clear();
                        thing.TryGetComp<CompAdvancedBuildings>().RegenCache();
                    }
                }
            }*/
            dirty = true;
        }

		public List<Thing> RegenCache(List<Thing> list, IntVec3 cell, float radius)
        {
			if (dirty)
            {
				return GetForCell(cell, radius);
            }
			return list;
        }

		public List<Thing> GetForCell(IntVec3 cell, float radius)
		{
			this.techBuildingsPerCell.Clear();
			CellWithRadius key = new CellWithRadius(cell, radius);
			List<Thing> list;
			if (!this.techBuildingsPerCell.TryGetValue(key, out list))
			{
				list = new List<Thing>();
				foreach (Thing thing in GenRadial.RadialDistinctThingsAround(cell, this.map, radius, false))
				{
					if (CheckBuildings.CountsAsTechBuilding(thing))
					{
						list.Add(thing);
						Log.Message("ADDED");
					}
				}
				this.techBuildingsPerCell[key] = list;
			}
			return list;
		}

		public static bool CountsAsTechBuilding(ThingDef def, Faction faction)
		{
			if (def.category == ThingCategory.Building && faction != null)
			{
				foreach (ResearchProjectDef r in def.researchPrerequisites)
				{
					return r.techLevel >= TechLevel.Industrial;
				}
				return false;
			}
			return false;
		}

		public static bool CountsAsTechBuilding(Thing t)
		{
			return CheckBuildings.CountsAsTechBuilding(t.def, t.Faction);
		}

		private Dictionary<CellWithRadius, List<Thing>> techBuildingsPerCell = new Dictionary<CellWithRadius, List<Thing>>();
	}
}
