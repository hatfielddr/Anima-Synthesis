using RimWorld;
using System.Collections.Generic;
using Verse;

namespace AnimaSynthesis
{
    class CompAdvancedBuildings : ThingComp
    {
        public CompProperties_AdvancedBuildings Props => (CompProperties_AdvancedBuildings)this.props;
        List<Thing> buildings = new List<Thing>();

        public bool CheckBuildings()
        {
            //trying something new - Caching the buildings
            buildings = this.parent.Map.GetComponent<CheckBuildings>().RegenCache(buildings, this.parent.Position, Props.radius);

            if (buildings != null && buildings.Count != 0)
                return true;
            return false;
        }
    }
}
