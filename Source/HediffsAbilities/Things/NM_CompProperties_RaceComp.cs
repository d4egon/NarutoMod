// Decompiled with JetBrains decompiler
// Type: NarutoMod.Things.CompProperties_RaceComp
// Assembly: NarutoMod, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A24FD7BF-E4B7-40C4-8848-97E48E1CC6B6
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\RimWorld\Mods\SovereignNarutoMod\Assemblies\NarutoMod.dll

using Verse;

namespace NarutoMod.Things
{
    public class NM_CompProperties_RaceComp : CompProperties
    {
        public float powerBase;
        public float powerGain;

        public NM_CompProperties_RaceComp(float powerBase, float powerGain)
        {
            compClass = typeof(NM_Comp_RaceComp);
            this.powerBase = powerBase;
            this.powerGain = powerGain;
        }
    }
}
