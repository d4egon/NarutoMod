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
