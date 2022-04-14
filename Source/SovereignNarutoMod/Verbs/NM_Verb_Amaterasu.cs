using RimWorld;
using Verse;

namespace NarutoMod.Verbs
{
    public class NM_Verb_Amaterasu : NM_Verb_AbilityHediff
    {
        public override bool ValidateTarget(LocalTargetInfo target, bool showMessages = true)
        {
            if (target == null || target.Pawn == null)
                return false;
            if (target.Pawn.FlammableNow)
                return base.ValidateTarget(target, showMessages);
            Messages.Message(Translator.Translate("NarutoMod.Messages.TargetInflammable"), MessageTypeDefOf.RejectInput, false);
            return false;
        }

        public override void WarmupComplete()
        {
            NM_BlackFireUtility.TryAttachFire(base.currentTarget.Pawn, 1f);
            base.WarmupComplete();
            CasterPawn.stances.stunner.StunFor(60, CasterPawn, false, false);
        }
    }
}
