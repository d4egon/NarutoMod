using RimWorld;
using System;
using Verse;

namespace SovereignNarutoMod.Hediffs
{
	public class HediffCompProperties_AddSkills : HediffCompProperties
	{
		public SkillDef skillDef;

		public int levels;

		public HediffCompProperties_AddSkills()
		{
			this.compClass = typeof(HediffComp_AddSkills);
		}
	}
}
