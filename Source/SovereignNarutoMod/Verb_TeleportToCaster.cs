using RimWorld;
using System;

namespace SovereignNarutoMod.Verbs
{
	public class Verb_TeleportToCaster : Verb_AbilityHediff
	{
		public override void WarmupComplete()
		{
			base.WarmupComplete();
			base.AddEffecterToMaintain(EffecterDefOf.Skip_Entry.Spawn(this.currentTarget.get_Pawn().get_Position(), this.get_CasterPawn().get_Map(), 1f), this.currentTarget.get_Cell(), 120, this.currentTarget.get_Pawn(), null);
			base.AddEffecterToMaintain(EffecterDefOf.Skip_Entry.Spawn(this.get_CasterPawn().get_Position(), this.get_CasterPawn().get_Map(), 1f), this.get_CasterPawn().get_Position(), 120, this.get_CasterPawn(), null);
			this.currentTarget.get_Pawn().set_Position(this.get_CasterPawn().get_Position());
			this.currentTarget.get_Pawn().stances.stunner.StunFor(120, this.get_CasterPawn(), false, false);
			this.currentTarget.get_Pawn().Notify_Teleported(true, true);
		}
	}
}
