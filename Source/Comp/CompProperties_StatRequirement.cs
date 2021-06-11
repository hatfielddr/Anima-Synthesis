using RimWorld;
using Verse;

namespace AnimaSynthesis
{
    class CompProperties_StatRequirement : CompProperties
    {
        public int level;
        public SkillDef skill = null;

        public CompProperties_StatRequirement()
        {
            this.compClass = typeof(CompStatRequirement);
        }
    }
}
