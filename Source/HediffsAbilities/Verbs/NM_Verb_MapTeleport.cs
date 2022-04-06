// Decompiled with JetBrains decompiler
// Type: NarutoMod.Verbs.Verb_MapTeleport
// Assembly: NarutoMod, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A24FD7BF-E4B7-40C4-8848-97E48E1CC6B6
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\RimWorld\Mods\SovereignNarutoMod\Assemblies\NarutoMod.dll

using NarutoMod.GUI;
using Verse;

namespace NarutoMod.Verbs
{
    public class NM_Verb_MapTeleport : NM_Verb_AbilityHediff
    {
        public override void WarmupComplete()
        {
            base.WarmupComplete();
            Find.WindowStack.Add(new NM_Window_MapTeleport(CasterPawn));
        }
    }

}
