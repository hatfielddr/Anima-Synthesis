using RimWorld;
using System.Collections.Generic;
using System.Linq;
using Verse;

namespace AnimaSynthesis
{
    class EggRateOnHappiness : ThingComp
    {
        private IEnumerable<Thing> Tree(Map map)
        {
            return map.listerThings.AllThings.OfType<AS_Plant>();
        }

        public override void CompTick()
        {
            AS_Plant plant = (AS_Plant)this.Tree(this.parent.Map).FirstOrDefault();
            if (plant == null)
            {
                return;
            }
            if (plant.IsHappy())
            {
                this.parent.TryGetComp<CompEggLayer>().Props.eggCountRange.max = this.parent.TryGetComp<CompEggLayer>().Props.eggCountRange.min * 2;
            }
            else
            {
                this.parent.TryGetComp<CompEggLayer>().Props.eggCountRange.max = this.parent.TryGetComp<CompEggLayer>().Props.eggCountRange.min;
            }
        }
    }
}
