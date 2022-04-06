using SovereignNarutoMod.Verbs;
using System;
using Verse;

namespace SovereignNarutoMod.Gizmos
{
	public class Command_HediffAbility : Command_VerbTarget
	{
		public Verb_AbilityHediff Verb
		{
			get
			{
				return this.verb as Verb_AbilityHediff;
			}
		}

		public override string TopRightLabel
		{
			get
			{
				return Math.Round((double)this.Verb.Props.powerCost, 1).ToString() + " ";
			}
		}
	}
}
