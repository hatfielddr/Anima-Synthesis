using RimWorld;
using Verse;
using AnimaSynthesis;

namespace AnimaSynthesis
{
    [DefOf]
    public static class AS_DefOf
    {
        [MayRequireRoyalty]
        public static ThingDef Plant_TreeAnimaSprout;
        public static ThingDef Plant_TreeAnimaElder;
        public static ThingDef AnimaCropCircle;
        public static JobDef AnimaSynthesis_TendToAnimaPlant;
        public static JobDef AnimaSynthesis_AscendPlant;
        public static JobDef AnimaSynthesis_AnimaSynthesis;
        //public static FactionDef Animus;
        //public static QuestScriptDef AS_Intro_Wimp;
        public static PawnKindDef SynthesisColonist;
    }
}
