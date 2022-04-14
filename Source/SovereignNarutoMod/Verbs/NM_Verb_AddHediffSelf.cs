using Verse;

namespace NarutoMod.Verbs
{

    public class NM_Verb_AddHediffSelf : NM_Verb_AbilityHediff
    {
        public override void WarmupComplete()
        {
            if (CasterPawn.Dead)
                return;
            base.WarmupComplete();
            if (CasterPawn.health.hediffSet.HasHediff(Props.hediffDef, false))
                CasterPawn.health.RemoveHediff(CasterPawn.health.hediffSet.hediffs.Find(x => x.def == Props.hediffDef));
            HealthUtility.AdjustSeverity(CasterPawn, Props.hediffDef, Props.severity);
            CasterPawn.stances.stunner.StunFor(60, CasterPawn, false, false);
        }
    }
}
