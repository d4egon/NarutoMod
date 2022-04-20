using RimWorld;
using Verse;

namespace NarutoMod.Verbs
{
    class NM_Verb_SummonThing_WoodClone : NM_Verb_AbilityHediff
    {
        public PawnKindDef pawnKindToSpawn = DefOfs.NM_PawnKindDefOf.NM_SummonPawnKind_WoodClone;

        public override bool ValidateTarget(LocalTargetInfo target, bool showMessages = true) => target != null && target.Pawn != null && base.ValidateTarget(target, showMessages);

        public override void WarmupComplete()
        {
            base.WarmupComplete();
            _ = GenSpawn.Spawn(PawnGenerator.GeneratePawn(
                               new PawnGenerationRequest(
                                 Props.pawnKindToSpawn,
                                 caster.Faction,
                                 newborn: false,
                                 allowAddictions: false,
                                 mustBeCapableOfViolence: true,
                                 allowGay: false,
                                 fixedBiologicalAge: CasterPawn.ageTracker.AgeBiologicalYearsFloat,
                                 fixedChronologicalAge: CasterPawn.ageTracker.AgeChronologicalYearsFloat,
                                 fixedGender: CasterPawn.gender,
                                 fixedLastName: CasterPawn.story.birthLastName,
                                 fixedMelanin: 1f,
                                 forceNoIdeo: true)
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