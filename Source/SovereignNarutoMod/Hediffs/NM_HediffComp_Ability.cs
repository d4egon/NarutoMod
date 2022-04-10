// Decompiled with JetBrains decompiler
// Type: NarutoMod.Hediffs.HediffComp_Ability
// Assembly: NarutoMod, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A24FD7BF-E4B7-40C4-8848-97E48E1CC6B6
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\RimWorld\Mods\SovereignNarutoMod\Assemblies\NarutoMod.dll

using NarutoMod.Things;
using Verse;

namespace NarutoMod.Hediffs
{

    public class NM_HediffComp_Ability : HediffComp
    {

        public NM_HediffCompProperties_Ability Props => props as NM_HediffCompProperties_Ability;


        private NM_Comp_RaceComp Comp_Race => parent.pawn.GetComp<NM_Comp_RaceComp>();


        public override void CompPostPostAdd(DamageInfo? dinfo)
        {
            base.CompPostPostAdd(dinfo);
            Comp_Race.InitVerbsFromZero();
        }

        public override void CompPostPostRemoved()
        {
            base.CompPostPostRemoved();
            Comp_Race.InitVerbsFromZero();
        }
    }
}
