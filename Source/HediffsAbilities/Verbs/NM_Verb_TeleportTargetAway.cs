using RimWorld;
using System.Collections.Generic;
using System.Linq;
using Verse;

namespace NarutoMod.Verbs
{

    public class NM_Verb_TeleportTargetAway : NM_Verb_AbilityHediff
    {
        public override void WarmupComplete()
        {
            base.WarmupComplete();
            AddEffecterToMaintain(EffecterDefOf.Skip_Entry.Spawn(currentTarget.Pawn.Position, CasterPawn.Map, 1f), currentTarget.Cell, 120, currentTarget.Pawn);
            AddEffecterToMaintain(EffecterDefOf.Skip_Entry.Spawn(CasterPawn.Position, CasterPawn.Map, 1f), CasterPawn.Position, 120, CasterPawn);
            List<IntVec3> list = GenRadial.RadialCellsAround(CasterPawn.Position, Props.range, false).Where(x => x.DistanceTo(CasterPawn.Position) > Props.range - 5.0 && GenGrid.Walkable(x, caster.Map)).ToList();
            if (!GenList.NullOrEmpty(list))
            {
                currentTarget.Pawn.Position = GenCollection.RandomElement(list);
                currentTarget.Pawn.stances.stunner.StunFor (120, CasterPawn, false, false);
                currentTarget.Pawn.Notify_Teleported(true, true);
                return;
            }
            else
            Messages.Message(Translator.Translate("NarutoMod.Messages.CellsNullOrEmpty"), MessageTypeDefOf.NeutralEvent, true);
        }
    }
}
