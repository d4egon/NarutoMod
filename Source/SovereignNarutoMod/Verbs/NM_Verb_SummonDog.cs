using RimWorld;
using Verse.AI;
using Verse;
using System.Collections.Generic;
using NarutoMod.Hediffs;
using System.Text;

namespace NarutoMod.Verbs
{
    class NM_Verb_SummonDog : NM_Verb_AbilityHediff
	{
		public PawnKindDef pawnKindToSpawn = DefOfs.NM_PawnKindDefOf.NM_SummonCreature;
		public override void WarmupComplete()
		{
			if (base.verbProps.spawnDef == null)
			{
				Log.Message("spawnDef == NULL @ NarutoMod.Verbs.NM_Verb_SummonDog");
			}
			if (base.CasterPawn.Map == null)
			{
				Log.Message("ERROR: CasterPawn.Map == NULL @ NarutoMod.Verbs.NM_Verb_SummonDog");
			}
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