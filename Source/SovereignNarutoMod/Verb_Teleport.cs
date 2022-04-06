using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Verse;

namespace SovereignNarutoMod.Verbs
{
	public class Verb_Teleport : Verb_AbilityHediff
	{
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			public static readonly Verb_Teleport.<>c<>9 = new Verb_Teleport.<>c();

		public static Func<IntVec3, bool> <>9__2_0;

			internal bool <WarmupComplete>b__2_0(IntVec3 x)
		{
			return x.get_IsValid();
		}
	}

	protected override float EffectiveRange
	{
		get
		{
			if (!base.Props.ignoreRange)
			{
				return base.get_EffectiveRange();
			}
			return 999f;
		}
	}

	public override void WarmupComplete()
	{
		base.WarmupComplete();
		base.AddEffecterToMaintain(EffecterDefOf.Skip_Entry.Spawn(this.get_CasterPawn().get_Position(), this.get_CasterPawn().get_Map(), 1f), this.currentTarget.get_Cell(), 120, this.get_CasterPawn(), null);
		base.AddEffecterToMaintain(EffecterDefOf.Skip_Entry.Spawn(this.currentTarget.get_Cell(), this.get_CasterPawn().get_Map(), 1f), this.currentTarget.get_Cell(), 120, this.get_CasterPawn(), null);
		IEnumerable<IntVec3> arg_B9_0 = GenRadial.RadialCellsAround(base.get_CasterPawn().get_Position(), 3f, true);
		Func<IntVec3, bool> arg_B9_1;
		if ((arg_B9_1 = Verb_Teleport.<> c.<> 9__2_0) == null)
		{
			arg_B9_1 = (Verb_Teleport.<> c.<> 9__2_0 = new Func<IntVec3, bool>(Verb_Teleport.<> c.<> 9.< WarmupComplete > b__2_0));
		}
		using (List<IntVec3>.Enumerator enumerator = arg_B9_0.Where(arg_B9_1).ToList<IntVec3>().GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				Pawn firstPawn = GridsUtility.GetFirstPawn(enumerator.Current, this.get_CasterPawn().get_Map());
				if (firstPawn != null)
				{
					firstPawn.jobs.StopAll(false, true);
					firstPawn.stances.stunner.StunFor(15, this.get_CasterPawn(), false, false);
				}
			}
		}
		this.get_CasterPawn().set_Position(this.currentTarget.get_Cell());
		this.get_CasterPawn().stances.stunner.StunFor(45, this.get_CasterPawn(), false, false);
		this.get_CasterPawn().Notify_Teleported(true, true);
	}

	public override void DrawHighlight(LocalTargetInfo target)
	{
		if (!base.Props.ignoreRange)
		{
			base.DrawHighlight(target);
		}
	}

	public override bool CanHitTargetFrom(IntVec3 root, LocalTargetInfo targ)
	{
		if (targ.get_Thing() != null && targ.get_Thing() == this.caster)
		{
			return this.get_targetParams().canTargetSelf;
		}
		return !base.ApparelPreventsShooting();
	}
}
}
