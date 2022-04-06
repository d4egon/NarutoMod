using System;
using Verse;

namespace SovereignNarutoMod.Verbs
{
	public class Verb_AddHediffTarget : Verb_AbilityHediff
	{
		public override bool ValidateTarget(LocalTargetInfo target, bool showMessages = true)
		{
			return !(target == null) && target.get_Pawn() != null && base.ValidateTarget(target, showMessages);
		}

		public override void WarmupComplete()
		{
			if (this.currentTarget.get_Pawn() == null)
			{
				return;
			}
			base.WarmupComplete();
			this.get_CasterPawn().stances.stunner.StunFor(60, this.get_CasterPawn(), false, false);
			if (this.currentTarget.get_Pawn().health.hediffSet.HasHediff(base.Props.hediffDef, false))
			{
				this.currentTarget.get_Pawn().health.RemoveHediff(this.get_CasterPawn().health.hediffSet.hediffs.Find((Hediff x) => x.def == base.Props.hediffDef));
			}
			HealthUtility.AdjustSeverity(this.currentTarget.get_Pawn(), base.Props.hediffDef, base.Props.severity);
		}
	}
}
