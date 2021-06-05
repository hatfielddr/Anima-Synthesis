using RimWorld;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public override string CompInspectStringExtra()
        {
            StringBuilder stringBuilder = new StringBuilder();
            if (this.CheckBuildings())
            {
                if (buildings.Count > 0)
                {
                    TaggedString taggedString = "GrowthImpacted".Translate() + ": " + (from c in buildings.Take(3)
                                                                                       select c.LabelShort).ToCommaList(false).CapitalizeFirst();
                    if (buildings.Count > 3)
                    {
                        taggedString += " " + "Etc".Translate();
                    }
                    stringBuilder.Append(taggedString);
                }
            }
            return stringBuilder.ToString();
        }
    }
}
