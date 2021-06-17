using RimWorld;
using System.Collections.Generic;
using System.Linq;
using Verse;

namespace AnimaSynthesis
{
    class CompSpawnPawnType : ThingComp
    {
        public CompProperties_SpawnPawnType Props => (CompProperties_SpawnPawnType)props;

        private IEnumerable<Thing> Candidates(Map map, int tile)
        {
            return from x in map.listerThings.AllThings
                   where x.def.defName == "AnimaCropCircle" && x.Tile == tile
                   select x;
        }

        public override void CompTick()
        {
            var pawn = ASUtility.GenerateASPawn_New(Props.pawnKind, this.parent.Tile);
            _ = GenSpawn.Spawn(pawn, this.parent.Position, this.parent.Map, Rot4.South) as Pawn;
            //Thing thing = (Thing)this.Candidates(this.parent.Map, this.parent.Tile).FirstOrDefault();
            //thing.Destroy();
            this.parent.Destroy();
        }
    }
}
