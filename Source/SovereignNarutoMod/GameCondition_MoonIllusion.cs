using RimWorld;
using System;
using System.Collections.Generic;
using UnityEngine;
using Verse;

namespace SovereignNarutoMod.GameConditions
{
	public class GameCondition_MoonIllusion : GameCondition
	{
		private SkyColorSet ToxicFalloutColors = new SkyColorSet(new ColorInt(216, 40, 0).get_ToColor(), new ColorInt(234, 200, 255).get_ToColor(), new Color(0.6f, 0.4f, 0.5f), 0.85f);

		public override void Init()
		{
			LessonAutoActivator.TeachOpportunity(ConceptDefOf.ForbiddingDoors, 2);
			LessonAutoActivator.TeachOpportunity(ConceptDefOf.AllowedAreas, 2);
		}

		public override void GameConditionTick()
		{
			Map singleMap = base.get_SingleMap();
			this.DoPawnsDamage(singleMap);
		}

		private void DoPawnsDamage(Map map)
		{
			List<Pawn> allPawnsSpawned = map.mapPawns.get_AllPawnsSpawned();
			for (int i = 0; i < allPawnsSpawned.Count; i++)
			{
				GameCondition_MoonIllusion.DoPawnDamage(allPawnsSpawned[i]);
			}
		}

		public static void DoPawnDamage(Pawn p)
		{
			if ((!p.get_Spawned() || !GridsUtility.Roofed(p.get_Position(), p.get_Map())) && p.get_RaceProps().get_IsFlesh())
			{
				float num = 3.3E-05f;
				num *= StatExtension.GetStatValue(p, StatDefOfLocal.StatDef_IllusionResistance, true);
				if (num != 0f)
				{
					float num2 = Mathf.Lerp(0.85f, 1.15f, Rand.ValueSeeded(p.thingIDNumber ^ 74374237));
					num *= num2;
					HealthUtility.AdjustSeverity(p, HediffDefOfLocal.HediffDef_Illusion, num);
				}
			}
		}

		public override float SkyTargetLerpFactor(Map map)
		{
			return GameConditionUtility.LerpInOutValue(this, (float)this.get_TransitionTicks(), 0.5f);
		}

		public override SkyTarget? SkyTarget(Map map)
		{
			return new SkyTarget?(new SkyTarget(0.85f, this.ToxicFalloutColors, 1f, 1f));
		}

		public override float AnimalDensityFactor(Map map)
		{
			return 0f;
		}

		public override float PlantDensityFactor(Map map)
		{
			return 0f;
		}

		public override bool AllowEnjoyableOutsideNow(Map map)
		{
			return false;
		}
	}
}
