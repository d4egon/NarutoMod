using SovereignNarutoMod.Things;
using RimWorld;
using System;
using System.Collections.Generic;
using Verse;

namespace SovereignNarutoMod.Hediffs
{
	public class Hediff_Illusion : HediffWithComps
	{
		public override void Notify_PawnDied()
		{
			base.Notify_PawnDied();
			CompRottable compRottable = ThingCompUtility.TryGetComp<CompRottable>(this.pawn.get_Corpse());
			if (compRottable != null)
			{
				compRottable.set_RotProgress((float)compRottable.get_PropsRot().get_TicksToDessicated());
			}
			List<Thing> list = this.pawn.get_Corpse().get_Map().listerThings.ThingsOfDef(ThingDefOfLocal.Plant_StrangeFruitTree);
			if (!GenList.NullOrEmpty<Thing>(list))
			{
				((Building_FruitTree)GenCollection.RandomElement<Thing>(list)).Fill(this.pawn.get_Corpse());
			}
		}
	}
}
