using RimWorld;
using System.Collections.Generic;
using Verse;

namespace AnimaSynthesis
{
    class CompAdvancedBuildings : ThingComp
    {
        public CompProperties_AdvancedBuildings Props => (CompProperties_AdvancedBuildings)this.props;

        public bool CheckBuildings()
        {
            List<Thing> things = this.parent.Map.GetComponent<CheckBuildings>().GetForCell(this.parent.Position, Props.radius);
            if (things.Count != 0)
                return true;
            return false;
        }
    }
}
