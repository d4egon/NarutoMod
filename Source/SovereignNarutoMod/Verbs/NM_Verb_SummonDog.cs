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
			GenSpawn.Spawn(NM_ThingDefOf.NM_SummonDog, base.currentTarget.Cell, base.caster.Map);
			
		}
	}
}