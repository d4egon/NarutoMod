using RimWorld;
using Verse.AI;
using Verse;
using System.Collections.Generic;
using NarutoMod.Hediffs;
using System.Text;
using NarutoMod.DefOfs;

namespace NarutoMod.Verbs
{
    class NM_Verb_SummonThing_Dog : NM_Verb_AbilityHediff
	{
		public PawnKindDef pawnKindToSpawn = DefOfs.NM_PawnKindDefOf.NM_SummonPawnKind_Dog;
		public override void WarmupComplete()
		{
			this.verbProps.targetParams.canTargetLocations.GetHashCode();

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