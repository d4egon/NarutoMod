using RimWorld;

namespace NarutoMod.Verbs
{
    public class NM_Verb_TeleportToCaster : NM_Verb_AbilityHediff
    {
        public override void WarmupComplete()
        {
            base.WarmupComplete();
            AddEffecterToMaintain(EffecterDefOf.Skip_Entry.Spawn(currentTarget.Pawn.Position, CasterPawn.Map, 1f), currentTarget.Cell, 120, currentTarget.Pawn);
            AddEffecterToMaintain(EffecterDefOf.Skip_Entry.Spawn(CasterPawn.Position, CasterPawn.Map, 1f), CasterPawn.Position, 120, CasterPawn);
            currentTarget.Pawn.Position = CasterPawn.Position;
            currentTarget.Pawn.stances.stunner.StunFor(120, CasterPawn, false, false);
            currentTarget.Pawn.Notify_Teleported(true, true);
        }
    }
}
