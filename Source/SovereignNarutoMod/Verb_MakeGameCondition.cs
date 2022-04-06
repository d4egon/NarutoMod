using RimWorld;
using System;
using UnityEngine;
using Verse;

namespace SovereignNarutoMod.Verbs
{
	public class Verb_MakeGameCondition : Verb_AbilityHediff
	{
		public override void WarmupComplete()
		{
			if (!this.get_CasterPawn().get_Dead())
			{
				base.WarmupComplete();
				GameConditionManager arg_55_0 = this.get_CasterPawn().get_Map().get_GameConditionManager();
				int num = Mathf.RoundToInt(base.Props.conditionDaysRange.get_RandomInRange() * 60000f);
				GameCondition gameCondition = GameConditionMaker.MakeCondition(base.Props.gameConditionDef, num);
				arg_55_0.RegisterCondition(gameCondition);
				Find.get_LetterStack().ReceiveLetter(base.Props.gameConditionDef.label, base.Props.gameConditionDef.description, LetterDefOf.ThreatBig, null);
			}
		}
	}
}
