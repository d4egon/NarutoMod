using SovereignNarutoMod.GUI;
using System;
using Verse;

namespace SovereignNarutoMod.Verbs
{
	public class Verb_MapTeleport : Verb_AbilityHediff
	{
		public override void WarmupComplete()
		{
			base.WarmupComplete();
			Find.get_WindowStack().Add(new Window_MapTeleport(this.get_CasterPawn()));
		}
	}
}
