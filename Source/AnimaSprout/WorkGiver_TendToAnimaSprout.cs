using RimWorld;
using Verse;
using Verse.AI;

namespace AnimaSynthesis
{
    class WorkGiver_TendToAnimaSprout : WorkGiver_Scanner
    {
        public override ThingRequest PotentialWorkThingRequest
        {
            get
            {
                return ThingRequest.ForDef(AS_DefOf.Plant_TreeAnimaSprout);
            }
        }

        public override PathEndMode PathEndMode
        {
            get
            {
                return PathEndMode.Touch;
            }
        }

        public override bool HasJobOnThing(Pawn pawn, Thing t, bool forced = false)
        {
            AS_Plant tNR_AnimaSprout = t as AS_Plant;
            if (tNR_AnimaSprout == null || !tNR_AnimaSprout.needTend)
            {
                return false;
            }
            if (t.IsBurning())
            {
                return false;
            }
            if (!t.IsForbidden(pawn))
            {
                LocalTargetInfo target = t;
                if (pawn.CanReserve(target, 1, -1, null, forced))
                {
                    return true;
                }
            }
            return false;
        }

        public override Job JobOnThing(Pawn pawn, Thing t, bool forced = false)
        {
            return new Job(AS_DefOf.AnimaSynthesis_TendToAnimaSprout, t);
        }
    }
}
