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
            base.WarmupComplete();

            //AddEffecterToMaintain(DefOfs.NM_EffecterDefOf.NM_SharinganRotating.Spawn(CasterPawn.Position, CasterPawn.Map, 1f), currentTarget.Cell, 120, CasterPawn);

            FireUtility.TryAttachFire(base.currentTarget.Pawn, 1f);
            
            CasterPawn.stances.stunner.StunFor(60, CasterPawn, false, false);
        }
    }
}
