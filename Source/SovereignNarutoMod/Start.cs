using HarmonyLib;
using System;
using Verse;

namespace SovereignNarutoMod.Patches
{
	[StaticConstructorOnStartup]
	public static class SNM_Start
	{
		static SNM_Start()
		{
			new Harmony("DimonSever000.SovereignNarutoMod").PatchAll();
		}
	}
}
