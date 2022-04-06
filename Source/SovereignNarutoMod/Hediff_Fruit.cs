using SovereignNarutoMod.Things;
using RimWorld;
using System;
using Verse;

namespace SovereignNarutoMod.Hediffs
{
	public class Hediff_Fruit : HediffWithComps
	{
		private float severityPerFruit
		{
			get
			{
				return 0.1f;
			}
		}

		private float powerGain
		{
			get
			{
				return 100f;
			}
		}

		private Comp_RaceComp comp_Race
		{
			get
			{
				return ThingCompUtility.TryGetComp<Comp_RaceComp>(this.pawn);
			}
		}

		public void Notify_FruitIngested()
		{
			this.set_Severity(this.get_Severity() + this.severityPerFruit);
			if (this.comp_Race != null)
			{
				this.comp_Race.Notify_PowerGain(this.powerGain);
			}
			if (this.pawn.def == ThingDefOfLocal.DivineEater)
			{
				this.pawn.health.RemoveHediff(this);
			}
			if (this.get_Severity() >= this.def.maxSeverity)
			{
				this.ChangeRace();
			}
		}

		private void ChangeRace()
		{
			Pawn pawn = PawnGenerator.GeneratePawn(new PawnGenerationRequest(PawnKindDefOfLocal.PawnKindDef_DivineEater, null, 2, -1, false, false, false, false, true, false, 1f, false, true, true, true, false, false, false, false, 0f, 0f, null, 1f, null, null, null, null, null, null, null, null, null, null, null, null, null, false, false, false));
			pawn.ageTracker = this.pawn.ageTracker;
			pawn.gender = this.pawn.gender;
			pawn.skills = this.pawn.skills;
			pawn.SetFaction(Find.get_FactionManager().get_OfPlayer(), null);
			pawn.health.AddHediff(HediffDefOfLocal.Prostheses_HediffDef_EyeVIII, GenCollection.RandomElement<BodyPartRecord>(pawn.health.hediffSet.GetNotMissingParts(0, 0, BodyPartTagDefOf.SightSource, null)), null, null);
			GenSpawn.Spawn(pawn, this.pawn.get_Position(), this.pawn.get_Map(), 0);
			this.pawn.equipment.DropAllEquipment(this.pawn.get_Position(), true);
			this.pawn.apparel.DropAll(this.pawn.get_Position(), true, true);
			this.pawn.inventory.DropAllNearPawn(this.pawn.get_Position(), false, false);
			this.pawn.Destroy(0);
		}
	}
}
