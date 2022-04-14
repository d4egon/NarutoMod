using RimWorld;
using Verse.AI;
using Verse;
using System.Collections.Generic;
using NarutoMod.Hediffs;


namespace NarutoMod.Verbs
{
    class NM_Verb_SummonDog : NM_Verb_AbilityHediff
	{

		public override void WarmupComplete()
		{
			base.WarmupComplete();
            CasterPawn.stances.stunner.StunFor(45, CasterPawn, false, false);

			if (this.currentTarget.Cell.Standable(caster.Map))
			{
				return;
			}
			GenSpawn.Spawn(base.verbProps.spawnDef, currentTarget.Cell, caster.Map, WipeMode.Vanish);
			if (verbProps.colonyWideTaleDef != null)
			{
				Pawn pawn = caster.Map.mapPawns.FreeColonistsSpawned.RandomElementWithFallback(null);
				TaleRecorder.RecordTale(verbProps.colonyWideTaleDef, new object[]
				{
					caster,
					pawn
				});
			}
		}
    }
}