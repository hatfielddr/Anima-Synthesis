using RimWorld;
using Verse;
using Verse.AI;

namespace AnimaSynthesis
{
    class WorkGiver_AscendPlant : WorkGiver_Scanner
    {

        public override ThingRequest PotentialWorkThingRequest
        {
            get
            {
                return ThingRequest.ForGroup(ThingRequestGroup.Plant);
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
            if (tNR_AnimaSprout == null || tNR_AnimaSprout.LifeStage != PlantLifeStage.Mature)
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
            if (pawn.skills != null && pawn.skills.GetSkill(SkillDefOf.Plants).Level < 12)
            {
                JobFailReason.Is("UnderAllowedSkill".Translate(12), this.def.label);
                return null;
            }
            return new Job(AS_DefOf.AnimaSynthesis_AscendPlant, t);
        }
    }
}
