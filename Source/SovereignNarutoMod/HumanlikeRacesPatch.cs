using SovereignNarutoMod.Things;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Verse;

namespace SovereignNarutoMod.Patches
{
	[StaticConstructorOnStartup]
	public static class HumanlikeRacesPatch
	{
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			public static readonly HumanlikeRacesPatch.<>c<>9 = new HumanlikeRacesPatch.<>c();

		internal bool cctor>b__0_0(ThingDef x)
		{
			return x.race != null && x.race.get_Humanlike() && !x.race.get_IsMechanoid();
		}

		internal bool cctor>b__0_1(CompProperties c)
		{
			return c is CompProperties_RaceComp;
		}
	}

	static HumanlikeRacesPatch()
	{
		List<ThingDef> arg_31_0 = DefDatabase<ThingDef>.get_AllDefsListForReading().Where(new Func<ThingDef, bool>(HumanlikeRacesPatch.<> c.<> 9.<.cctor > b__0_0)).ToList<ThingDef>();
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.AppendLine("Hediff abilities races patch loaded successfully, races affected: ");
		foreach (ThingDef current in arg_31_0)
		{
			if (current.comps.Find(new Predicate<CompProperties>(HumanlikeRacesPatch.<> c.<> 9.<.cctor > b__0_1)) == null)
			{
				current.comps.Add(new CompProperties_RaceComp(300f, 0.025f));
				stringBuilder.Append(current.defName + "; ");
			}
		}
		Log.Message(stringBuilder.ToString());
	}
}
}
