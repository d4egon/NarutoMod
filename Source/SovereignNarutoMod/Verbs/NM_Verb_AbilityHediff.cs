// Decompiled with JetBrains decompiler
// Type: NarutoMod.Verbs.Verb_AbilityHediff
// Assembly: NarutoMod, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A24FD7BF-E4B7-40C4-8848-97E48E1CC6B6
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\RimWorld\Mods\SovereignNarutoMod\Assemblies\NarutoMod.dll

using NarutoMod.Things;
using System.Collections.Generic;
using Verse;
using Verse.AI;

namespace NarutoMod.Verbs
{
    public abstract class NM_Verb_AbilityHediff : Verb_CastBase
    {
        public List<Pair<Effecter, TargetInfo>> maintainedEffecters = new List<Pair<Effecter, TargetInfo>>();

        private NM_Comp_RaceComp Comp => CasterPawn.GetComp<NM_Comp_RaceComp>();

        public NM_VerbProperties_Ability Props => verbProps as NM_VerbProperties_Ability;

        public int Tick => Find.TickManager.TicksGame;

        public virtual bool IsReady() => (double)Comp.Power >= Props.powerCost;

        public override bool Available() => IsReady() && base.Available();

        public override void ExposeData()
        {
        }

        protected override bool TryCastShot() => true;

        public override void WarmupComplete()
        {
            base.WarmupComplete();
            Comp.Notify_PowerGain(Props.powerCost);
        }

        public void AddEffecterToMaintain(Effecter eff, IntVec3 pos, int ticks, Pawn pawn, Map map = null)
        {
            eff.ticksLeft = ticks;
            maintainedEffecters.Add(new Pair<Effecter, TargetInfo>(eff, new TargetInfo(pos, map ?? pawn.Map, false)));
        }
    }
}
