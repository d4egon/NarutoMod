using SovereignNarutoMod.Things;
using System;
using System.Collections.Generic;
using Verse;
using Verse.AI;

namespace SovereignNarutoMod.Verbs
{
	public abstract class Verb_AbilityHediff : Verb_CastBase
	{
		public List<Pair<Effecter, TargetInfo>> maintainedEffecters = new List<Pair<Effecter, TargetInfo>>();

		private Comp_RaceComp comp
		{
			get
			{
				return this.get_CasterPawn().GetComp<Comp_RaceComp>();
			}
		}

		public VerbProperties_Ability Props
		{
			get
			{
				return this.verbProps as VerbProperties_Ability;
			}
		}

		public int Tick
		{
			get
			{
				return Find.get_TickManager().get_TicksGame();
			}
		}

		public virtual bool IsReady()
		{
			return this.comp.Power >= this.Props.powerCost;
		}

		public override bool Available()
		{
			return this.IsReady() && base.Available();
		}

		public override void ExposeData()
		{
		}

		protected override bool TryCastShot()
		{
			return true;
		}

		public override void WarmupComplete()
		{
			base.WarmupComplete();
			this.comp.Notify_PowerGain(-this.Props.powerCost);
		}

		public void AddEffecterToMaintain(Effecter eff, IntVec3 pos, int ticks, Pawn pawn, Map map = null)
		{
			eff.ticksLeft = ticks;
			this.maintainedEffecters.Add(new Pair<Effecter, TargetInfo>(eff, new TargetInfo(pos, map ?? pawn.get_Map(), false)));
		}
	}
}
