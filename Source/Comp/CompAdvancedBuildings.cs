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
            buildings = this.parent.Map.GetComponent<CheckBuildings>().RegenCache(buildings, this.parent.Position, Props.radius);
            //trying something new
            if (buildings.Count != 0)
                return true;
            return false;
        }

        public void RegenCache()
        {
            buildings = this.parent.Map.GetComponent<CheckBuildings>().GetForCell(this.parent.Position, Props.radius);
        }
    }
}
