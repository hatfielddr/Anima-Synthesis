using RimWorld;
using Verse;

namespace AnimaSynthesis
{
    class CompMovingGlower : ThingComp
    {
        public int nextUpdateTick = 0;
        public Thing light;

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
			bool flag = !this.light.DestroyedOrNull();
			if (flag)
			{
				this.light.Destroy(DestroyMode.Vanish);
				this.light = null;
			}
		}

		public override void CompTick()
		{
			base.CompTick();
			//if (Find.TickManager.TicksGame >= this.nextUpdateTick)
			//{
			//	this.nextUpdateTick = Find.TickManager.TicksGame + 60;
				if (this.parent.Spawned)
				{
					this.SpawnLight();
				}
			//}
		}
	}
}
