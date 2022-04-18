using RimWorld;
using Verse.AI;
using Verse;
using System.Collections.Generic;
using NarutoMod.Hediffs;
using System.Text;
using NarutoMod.DefOfs;

namespace NarutoMod.Verbs
{
    class NM_Verb_SummonThing_Toad : NM_Verb_AbilityHediff
	{
		public override bool ValidateTarget(LocalTargetInfo target, bool showMessages = true) => target != null && target.Pawn != null && base.ValidateTarget(target, showMessages);
		public PawnKindDef pawnKindToSpawn = DefOfs.NM_PawnKindDefOf.NM_SummonPawnKind_Toad;
		public override void WarmupComplete()
		{

			base.WarmupComplete();
            base.CasterPawn.stances.stunner.StunFor(45, CasterPawn, false, false);
			GenSpawn.Spawn(PawnGenerator.GeneratePawn(
							new PawnGenerationRequest(
								Props.pawnKindToSpawn,
								caster.Faction,
								newborn: false,
								allowAddictions: false)
						),
						caster.Position,
						caster.Map
					);
		}
	}
}