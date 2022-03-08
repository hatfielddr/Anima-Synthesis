using RimWorld;
using Verse;
using System.Collections.Generic;

namespace AnimaSynthesis
{
    class CheckBuildings : MapComponent
    {
		public CheckBuildings(Map m) : base(m) { }
		public bool dirty = false;

		// Triggered when a building is placed or removed
		public void Notify_BuildingChange()
		{
            		dirty = true;
		}

		// Check if cache is dirty
		public List<Thing> RegenCache(List<Thing> list, IntVec3 cell, float radius)
        	{
			if (dirty)
            		{
				// If it is, regen it
				dirty = false;
				return GetForCell(cell, radius);
            		}
			// Else return the old cache
			return list;
        	}

		private List<Thing> GetForCell(IntVec3 cell, float radius)
		{
			this.techBuildingsPerCell.Clear();
			CellWithRadius key = new CellWithRadius(cell, radius);
			List<Thing> list = new List<Thing>();
			
			// Make sure the function doesn't return anything funky
			if (!this.techBuildingsPerCell.TryGetValue(key, out list))
			{
				list = new List<Thing>();
				foreach (Thing thing in GenRadial.RadialDistinctThingsAround(cell, this.map, radius, false))
				{
					if (CountsAsTechBuilding(thing))
					{
						// If the building is advanced enough, add it to that building's list
						list.Add(thing);
					}
				}
				this.techBuildingsPerCell[key] = list;
			}
			return list;
		}

		private bool CountsAsTechBuilding(ThingDef def, Faction faction)
		{
			// Check that the player actually owns the building
			if (def.category == ThingCategory.Building && faction != null)
			{
				// Verify that the building is research gated (not tribal level)
				if (def.researchPrerequisites.CountAllowNull() > 0)
				{
					Thing thing = ThingMaker.MakeThing(AS_DefOf.Plant_TreeAnimaSprout);
					foreach (ResearchProjectDef r in def.researchPrerequisites)
					{
						// Check if the tech level of any of the research prereqs are too advanced
						return r.techLevel >= thing.TryGetComp<CompAdvancedBuildings>().Props.techLevel;
					}
				}
				return false;
			}
			return false;
		}

		// Helper function for times when faction isn't known in the scope of the code
		public bool CountsAsTechBuilding(Thing t)
		{
			return CountsAsTechBuilding(t.def, t.Faction);
		}

		private Dictionary<CellWithRadius, List<Thing>> techBuildingsPerCell = new Dictionary<CellWithRadius, List<Thing>>();
	}
}
