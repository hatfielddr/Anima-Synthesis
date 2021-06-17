using RimWorld;
using Verse;

namespace AnimaSynthesis
{
    class CompProperties_SpawnPawnType : CompProperties
    {
        public CompProperties_SpawnPawnType()
        {
            this.compClass = typeof(CompSpawnPawnType);
        }

        public PawnKindDef pawnKind;
    }
}
