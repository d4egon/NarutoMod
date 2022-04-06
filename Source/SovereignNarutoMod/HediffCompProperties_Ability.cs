using SovereignNarutoMod.Verbs;
using System;
using System.Collections.Generic;
using Verse;

namespace SovereignNarutoMod.Hediffs
{
	public class HediffCompProperties_Ability : HediffCompProperties
	{
		public List<VerbProperties_Ability> verbProps = new List<VerbProperties_Ability>();

		public HediffCompProperties_Ability()
		{
			this.compClass = typeof(HediffComp_Ability);
		}
	}
}
