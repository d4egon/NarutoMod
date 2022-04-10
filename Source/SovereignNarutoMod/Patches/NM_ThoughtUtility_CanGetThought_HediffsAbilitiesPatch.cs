// Decompiled with JetBrains decompiler
// Type: NarutoMod.Patches.ThoughtUtility_CanGetThought_NarutoModPatch
// Assembly: NarutoMod, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A24FD7BF-E4B7-40C4-8848-97E48E1CC6B6
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\RimWorld\Mods\SovereignNarutoMod\Assemblies\NarutoMod.dll
/*
using HarmonyLib;
using RimWorld;
using Verse;

namespace NarutoMod.Patches
{
  [HarmonyPatch(typeof (ThoughtUtility))]
  [HarmonyPatch("CanGetThought")]
    public class NM_ThoughtUtility_CanGetThought_NarutoModPatch
  {
    private static void Postfix(
      Pawn pawn,
      ref bool __result)
    {
      if (!__result ||  pawn.def != NM_ThingDefOf.DivineEater)
        return;
      __result = false;
    }
  }
}

*/
