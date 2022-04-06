using RimWorld;
using System;

namespace SovereignNarutoMod.Verbs
{
	public class Verb_ThrowMeteor : Verb_AbilityHediff
	{
		public override void WarmupComplete()
		{
			base.WarmupComplete();
			SkyfallerMaker.SpawnSkyfaller(ThingDefOf.MeteoriteIncoming, this.currentTarget.get_Cell(), this.get_CasterPawn().get_Map());
			this.get_CasterPawn().stances.stunner.StunFor(45, this.get_CasterPawn(), false, false);
		}
	}
}
