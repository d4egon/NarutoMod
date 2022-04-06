using System;
using Verse;

namespace SovereignNarutoMod.Things
{
	public class CompProperties_RaceComp : CompProperties
	{
		public float powerBase;

		public float powerGain;

		public CompProperties_RaceComp(float powerBase, float powerGain)
		{
			this.compClass = typeof(Comp_RaceComp);
			this.powerBase = powerBase;
			this.powerGain = powerGain;
		}
	}
}
