using HarmonyLib;
using RimWorld;
using System;
using Verse;

namespace SovereignNarutoMod.Patches
{
	[HarmonyPatch(typeof(ThoughtUtility)), HarmonyPatch("CanGetThought")]
	public class ThoughtUtility_CanGetThought_SovereignNarutoModPatch
	{
		private static void Postfix(Pawn pawn, ThoughtDef def, ref bool checkIfNullified, ref bool __result)
		{
			if (__result && pawn.def == ThingDefOfLocal.DivineEater)
			{
				__result = false;
			}
		}
	}
}
