using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Verse;

namespace SovereignNarutoMod.Verbs
{
	public class Verb_Wallrise : Verb_AbilityHediff
	{
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			public static readonly Verb_Wallrise.<>c<>9 = new Verb_Wallrise.<>c();

		public static Func<Thing, bool> <>9__0_1;

			internal bool <WarmupComplete>b__0_1(Thing t)
		{
			return t.def.category == 2;
		}
	}

	public override void WarmupComplete()
	{
		base.WarmupComplete();
		Map map = this.get_CasterPawn().get_Map();
		List<Thing> list = new List<Thing>();
		list.AddRange(this.AffectedCells(base.get_CurrentTarget(), map).SelectMany(delegate (IntVec3 c)
		{
			IEnumerable<Thing> arg_2B_0 = GridsUtility.GetThingList(c, map);
			Func<Thing, bool> arg_2B_1;
			if ((arg_2B_1 = Verb_Wallrise.<> c.<> 9__0_1) == null)
			{
				arg_2B_1 = (Verb_Wallrise.<> c.<> 9__0_1 = new Func<Thing, bool>(Verb_Wallrise.<> c.<> 9.< WarmupComplete > b__0_1));
			}
			return arg_2B_0.Where(arg_2B_1);
		}));
		using (List<Thing>.Enumerator enumerator = list.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				enumerator.Current.DeSpawn(0);
			}
		}
		foreach (IntVec3 current in this.AffectedCells(base.get_CurrentTarget(), map))
		{
			GenSpawn.Spawn(ThingDefOfLocal.ThingDef_RaisedWood, current, map, 0);
		}
		foreach (Thing current2 in list)
		{
			IntVec3 intVec = IntVec3.get_Invalid();
			for (int i = 0; i < 9; i++)
			{
				IntVec3 intVec2 = current2.get_Position() + GenRadial.RadialPattern[i];
				if (GenGrid.InBounds(intVec2, map) && GenGrid.Walkable(intVec2, map) && map.thingGrid.ThingsListAtFast(intVec2).Count <= 0)
				{
					intVec = intVec2;
					break;
				}
			}
			if (intVec != IntVec3.get_Invalid())
			{
				GenSpawn.Spawn(current2, intVec, map, 0);
			}
			else
			{
				GenPlace.TryPlaceThing(current2, current2.get_Position(), map, 1, null, null, default(Rot4));
			}
		}
	}

	[IteratorStateMachine(typeof(Verb_Wallrise.< AffectedCells > d__1))]
	private IEnumerable<IntVec3> AffectedCells(LocalTargetInfo target, Map map)
	{
		Verb_Wallrise.< AffectedCells > d__1 expr_07 = new Verb_Wallrise.< AffectedCells > d__1(-2);
		expr_07.<> 4__this = this;
		expr_07.<> 3__target = target;
		expr_07.<> 3__map = map;
		return expr_07;
	}
}
}
