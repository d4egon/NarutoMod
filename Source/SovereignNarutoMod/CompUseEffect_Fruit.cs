using SovereignNarutoMod.Hediffs;
using RimWorld;
using System;
using Verse;

namespace SovereignNarutoMod.Things
{
	public class CompUseEffect_Fruit : CompUseEffect
	{
		public override void DoEffect(Pawn usedBy)
		{
			base.DoEffect(usedBy);
			Hediff_Fruit hediff_Fruit = (Hediff_Fruit)usedBy.health.hediffSet.GetFirstHediffOfDef(HediffDefOfLocal.HediffDef_Fruit, false);
			if (hediff_Fruit == null)
			{
				hediff_Fruit = (Hediff_Fruit)usedBy.health.AddHediff(HediffDefOfLocal.HediffDef_Fruit, null, null, null);
			}
			hediff_Fruit.Notify_FruitIngested();
		}
	}
}
