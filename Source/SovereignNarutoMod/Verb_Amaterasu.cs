using RimWorld;
using System;
using Verse;

namespace SovereignNarutoMod.Verbs
{
	public class Verb_Amaterasu : Verb_AbilityHediff
	{
		public override bool ValidateTarget(LocalTargetInfo target, bool showMessages = true)
		{
			if (target == null || target.get_Pawn() == null)
			{
				return false;
			}
			if (!target.get_Pawn().get_FlammableNow())
			{
				Messages.Message(Translator.Translate("NarutoMod.Messages.TargetUnflammable"), MessageTypeDefOf.RejectInput, false);
				return false;
			}
			return base.ValidateTarget(target, showMessages);
		}

		public override void WarmupComplete()
		{
			if (this.currentTarget.get_Pawn() == null)
			{
				return;
			}
			AmaterasuFireUtility.TryAttachFire(this.currentTarget.get_Pawn(), 1f);
			base.WarmupComplete();
			this.get_CasterPawn().stances.stunner.StunFor(60, this.get_CasterPawn(), false, false);
		}
	}
}
