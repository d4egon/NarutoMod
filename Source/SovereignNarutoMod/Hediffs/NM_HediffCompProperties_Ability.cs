// Decompiled with JetBrains decompiler
// Type: NarutoMod.Hediffs.HediffCompProperties_Ability
// Assembly: NarutoMod, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A24FD7BF-E4B7-40C4-8848-97E48E1CC6B6
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\RimWorld\Mods\SovereignNarutoMod\Assemblies\NarutoMod.dll

using NarutoMod.Verbs;
using System.Collections.Generic;
using Verse;

namespace NarutoMod.Hediffs
{
    public class NM_HediffCompProperties_Ability : HediffCompProperties
    {
        public List<NM_VerbProperties_Ability> verbProps = new List<NM_VerbProperties_Ability>();

        public NM_HediffCompProperties_Ability() => compClass = typeof(NM_HediffComp_Ability);
    }
}
