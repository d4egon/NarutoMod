using RimWorld;
using System.Linq;
using Verse;

namespace NarutoMod.Verbs
{
    public class NM_Verb_Teleport : NM_Verb_AbilityHediff
    {
        protected override float EffectiveRange => !Props.ignoreRange ? base.EffectiveRange : 999f;

        public override void WarmupComplete()
        {
            base.WarmupComplete();
            AddEffecterToMaintain(EffecterDefOf.Skip_Entry.Spawn(CasterPawn.Position, CasterPawn.Map, 1f), currentTarget.Cell, 120, CasterPawn);
            AddEffecterToMaintain(EffecterDefOf.Skip_Entry.Spawn(currentTarget.Cell, CasterPawn.Map, 1f), currentTarget.Cell, 120, CasterPawn);
            foreach (IntVec3 intVec3 in GenRadial.RadialCellsAround(CasterPawn.Position, 3f, true).Where(x => x.IsValid).ToList())
            {
                Pawn firstPawn = GridsUtility.GetFirstPawn(intVec3, CasterPawn.Map);
                if (firstPawn != null)
                {
                    firstPawn.jobs.StopAll(false, true);
                    firstPawn.stances.stunner.StunFor(15, CasterPawn, false, false);
                }
            }
            CasterPawn.Position = currentTarget.Cell;
            CasterPawn.stances.stunner.StunFor(45, CasterPawn, false, false);
            CasterPawn.Notify_Teleported(true, true);
        }

        public override void DrawHighlight(LocalTargetInfo target)
        {
            if (Props.ignoreRange)
                return;
            base.DrawHighlight(target);
        }

        public override bool CanHitTargetFrom(IntVec3 root, LocalTargetInfo targ)
        {
            if (targ.Thing != null && targ.Thing == caster)
                return targetParams.canTargetSelf;
            return !ApparelPreventsShooting();
        }
    }
}
