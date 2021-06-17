using RimWorld;
using System.Collections.Generic;
using Verse;

namespace AnimaSynthesis
{
    class CompAscendDescend : ThingComp
    {
        public CompProperties_AscendDescend Props => (CompProperties_AscendDescend)this.props;

        public ThingDef GetHigherRank()
        {
            return Props.higherRank;
        }

        public ThingDef GetLowerRank()
        {
            return Props.lowerRank;
        }
    }
}
