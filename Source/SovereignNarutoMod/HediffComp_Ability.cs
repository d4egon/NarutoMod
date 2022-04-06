using SovereignNarutoMod.Things;
using System;
using Verse;

namespace SovereignNarutoMod.Hediffs
{
	public class HediffComp_Ability : HediffComp
	{
		public HediffCompProperties_Ability Props
		{
			get
			{
				return this.props as HediffCompProperties_Ability;
			}
		}

		private Comp_RaceComp comp_Race
		{
			get
			{
				return this.parent.pawn.GetComp<Comp_RaceComp>();
			}
		}

		public override void CompPostPostAdd(DamageInfo? dinfo)
		{
			base.CompPostPostAdd(dinfo);
			this.comp_Race.InitVerbsFromZero();
		}

		public override void CompPostPostRemoved()
		{
			base.CompPostPostRemoved();
			this.comp_Race.InitVerbsFromZero();
		}
	}
}
