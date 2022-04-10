// Decompiled with JetBrains decompiler
// Type: NarutoMod.Hediffs.HediffCompProperties_AddSkills
// Assembly: NarutoMod, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A24FD7BF-E4B7-40C4-8848-97E48E1CC6B6
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\RimWorld\Mods\SovereignNarutoMod\Assemblies\NarutoMod.dll

using RimWorld;
using Verse;

namespace NarutoMod.Hediffs
{
    public class NM_HediffCompProperties_AddSkills : HediffCompProperties
    {
        public SkillDef skillDef;
        public int levels;

        public NM_HediffCompProperties_AddSkills() => compClass = typeof(NM_HediffComp_AddSkills);
    }
}
