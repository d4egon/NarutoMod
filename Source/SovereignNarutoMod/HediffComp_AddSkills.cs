using RimWorld;
using System;
using UnityEngine;
using Verse;

namespace SovereignNarutoMod.Hediffs
{
	public class HediffComp_AddSkills : HediffComp
	{
		public HediffCompProperties_AddSkills Props
		{
			get
			{
				return this.props as HediffCompProperties_AddSkills;
			}
		}

		public override void CompPostPostAdd(DamageInfo? dinfo)
		{
			base.CompPostPostAdd(dinfo);
			SkillRecord skillRecord = this.parent.pawn.skills.skills.Find((SkillRecord x) => x.def == this.Props.skillDef);
			if (!skillRecord.get_TotallyDisabled())
			{
				skillRecord.set_Level(Mathf.Clamp(skillRecord.get_Level() + this.Props.levels, 0, 20));
			}
		}

		public override void CompPostPostRemoved()
		{
			base.CompPostPostRemoved();
			SkillRecord skillRecord = this.parent.pawn.skills.skills.Find((SkillRecord x) => x.def == this.Props.skillDef);
			if (!skillRecord.get_TotallyDisabled())
			{
				skillRecord.set_Level(Mathf.Clamp(skillRecord.get_Level() - this.Props.levels, 0, 20));
			}
		}
	}
}
