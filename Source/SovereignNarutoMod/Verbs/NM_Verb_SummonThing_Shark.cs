using RimWorld;
using Verse;

namespace NarutoMod.Verbs
{
    class NM_Verb_SummonThing_Shark : NM_Verb_AbilityHediff
    {
        public PawnKindDef pawnKindToSpawn = DefOfs.NM_PawnKindDefOf.NM_SummonPawnKind_Shark;

        public override bool ValidateTarget(LocalTargetInfo target, bool showMessages = true) => target != null && target.Pawn != null && base.ValidateTarget(target, showMessages);

        public override void WarmupComplete()
        {
            base.WarmupComplete();
            _ = GenSpawn.Spawn(PawnGenerator.GeneratePawn(
                               new PawnGenerationRequest(
                                 Props.pawnKindToSpawn,
                                 caster.Faction,
                                 newborn: false,
                                 allowAddictions: false)
                                 ),
                              caster.Position,
                              caster.Map
                              );
            base.CasterPawn.stances.stunner.StunFor(45, CasterPawn, false, false);
            float smokeRadius = 5f;
            Map map = CasterPawn.Map;
            GenExplosion.DoExplosion(CurrentTarget.Cell, map, smokeRadius, DamageDefOf.Crush, null, 0, 1.2f, DefOfs.NM_SoundDefOf.NM_SummoningPoof, null, null, null, ThingDefOf.Gas_Smoke, 1f, 1, false, null, 0f, 1, 0f, false, null, null);
        }
    }
}