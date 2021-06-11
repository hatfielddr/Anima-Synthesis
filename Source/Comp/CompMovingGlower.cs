using RimWorld;
using Verse;

namespace AnimaSynthesis
{
    class CompMovingGlower : ThingComp
    {
        public int nextUpdateTick = 0;
        public Thing light = null;

		public void SpawnLight()
		{
			IntVec3 intVec = this.parent.Position;
			if (!this.light.DestroyedOrNull() && intVec != this.light.Position)
			{
				this.DestroyLight();
			}
			if (this.light.DestroyedOrNull())
			{
				Thing firstThing = intVec.GetFirstThing(this.parent.Map, Util_GlowerLight.GlowerLightDef);
				if (firstThing == null)
				{
					this.light = GenSpawn.Spawn(Util_GlowerLight.GlowerLightDef, intVec, this.parent.Map);
				}
			}
		}

		public void DestroyLight()
		{
			if (!this.light.DestroyedOrNull())
			{
				this.light.Destroy(DestroyMode.Vanish);
				this.light = null;
			}
		}

		public override void CompTick()
		{
			base.CompTick();
			if (this.parent.Spawned)
			{
				this.SpawnLight();
			}
            else
            {
				this.DestroyLight();
            }
		}

        public override void PostExposeData()
        {
            base.PostExposeData();
			Scribe_Values.Look<Thing>(ref this.light, "light", null, false);
		}
    }
}
