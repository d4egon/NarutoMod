// Decompiled with JetBrains decompiler
// Type: NarutoMod.Verbs.Verb_AddHediffTarget
// Assembly: NarutoMod, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A24FD7BF-E4B7-40C4-8848-97E48E1CC6B6
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\RimWorld\Mods\SovereignNarutoMod\Assemblies\NarutoMod.dll

using Verse;

namespace NarutoMod.Verbs
{

    public class NM_Verb_AddHediffTarget : NM_Verb_AbilityHediff
    {
        public override bool ValidateTarget(LocalTargetInfo target, bool showMessages = true) => target != null && target.Pawn != null && base.ValidateTarget(target, showMessages);

        public override void WarmupComplete()
        {
            if (currentTarget.Pawn == null)
            return;
            base.WarmupComplete();
            CasterPawn.stances.stunner.StunFor(60, CasterPawn, false, false);
            if (currentTarget.Pawn.health.hediffSet.HasHediff(Props.hediffDef, false))
                currentTarget.Pawn.health.RemoveHediff(CasterPawn.health.hediffSet.hediffs.Find(x => x.def == Props.hediffDef));
            HealthUtility.AdjustSeverity(currentTarget.Pawn, Props.hediffDef, Props.severity);
        }
    }

}
