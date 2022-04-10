// Decompiled with JetBrains decompiler
// Type: NarutoMod.Patches.Start
// Assembly: NarutoMod, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A24FD7BF-E4B7-40C4-8848-97E48E1CC6B6
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\RimWorld\Mods\SovereignNarutoMod\Assemblies\NarutoMod.dll

using HarmonyLib;
using Verse;

namespace NarutoMod.Patches
{
    [StaticConstructorOnStartup]
    public static class NM_Start
    {
        static NM_Start() => new Harmony("Aritocrats_Edo.NarutoMod").PatchAll();
    }
}
