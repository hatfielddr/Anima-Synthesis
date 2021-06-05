using RimWorld;
using Verse;
using System.Collections.Generic;

namespace AnimaSynthesis
{
    class CheckBuildings : MapComponent
    {
		public CheckBuildings(Map m) : base(m) { }
		public bool dirty = false;

		public void Notify_BuildingChange()
		{
            dirty = true;
        }

		public List<Thing> RegenCache(List<Thing> list, IntVec3 cell, float radius)
        {
			if (dirty)
            {
				dirty = false;
				return GetForCell(cell, radius);
            }
			return list;
        }

		private List<Thing> GetForCell(IntVec3 cell, float radius)
		{
			this.techBuildingsPerCell.Clear();
			CellWithRadius key = new CellWithRadius(cell, radius);
			List<Thing> list = new List<Thing>();
			if (!this.techBuildingsPerCell.TryGetValue(key, out list))
			{
				list = new List<Thing>();
				foreach (Thing thing in GenRadial.RadialDistinctThingsAround(cell, this.map, radius, false))
				{
					if (CountsAsTechBuilding(thing))
					{
						list.Add(thing);
					}
				}
				this.techBuildingsPerCell[key] = list;
			}
			return list;
		}

		private bool CountsAsTechBuilding(ThingDef def, Faction faction)
		{
			if (def.category == ThingCategory.Building && faction != null)
			{
				if (def.researchPrerequisites.CountAllowNull() > 0)
				{
					Thing thing = ThingMaker.MakeThing(AS_DefOf.Plant_TreeAnimaSprout);
					foreach (ResearchProjectDef r in def.researchPrerequisites)
					{
						return r.techLevel >= thing.TryGetComp<CompAdvancedBuildings>().Props.techLevel;
					}
				}
				return false;
			}
			return false;
		}

		public bool CountsAsTechBuilding(Thing t)
		{
			return CountsAsTechBuilding(t.def, t.Faction);
		}

		private Dictionary<CellWithRadius, List<Thing>> techBuildingsPerCell = new Dictionary<CellWithRadius, List<Thing>>();
	}
}
