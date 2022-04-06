﻿using System;
using System.Collections.Generic;
using UnityEngine;
using Verse;
using RimWorld;

namespace SovereignNarutoMod
{
	public static class AmaterasuFireUtility
	{
		private static readonly SimpleCurve ChanceToCatchFirePerSecondForPawnFromFlammability = new SimpleCurve
		{
			{
				new CurvePoint(0f, 0f),
				true
			},
			{
				new CurvePoint(0.1f, 0.07f),
				true
			},
			{
				new CurvePoint(0.3f, 1f),
				true
			},
			{
				new CurvePoint(1f, 1f),
				true
			}
		};

		public static bool CanEverAttachFire(this Thing t)
		{
			return !t.Destroyed && t.FlammableNow && t.def.category == ThingCategory.Pawn && t.TryGetComp<CompAttachBase>() != null;
		}

		public static float ChanceToStartFireIn(IntVec3 c, Map map)
		{
			List<Thing> thingList = c.GetThingList(map);
			float num = c.TerrainFlammableNow(map) ? c.GetTerrain(map).GetStatValueAbstract(StatDefOf.Flammability, null) : 0f;
			for (int i = 0; i < thingList.Count; i++)
			{
				Thing thing = thingList[i];
				if (thing is AmaterasuFire)
				{
					return 0f;
				}
				if (thing.def.category != ThingCategory.Pawn && thingList[i].FlammableNow)
				{
					num = Mathf.Max(num, thing.GetStatValue(StatDefOf.Flammability, true));
				}
			}
			if (num > 0f)
			{
				Building edifice = c.GetEdifice(map);
				if (edifice != null && edifice.def.passability == Traversability.Impassable && edifice.OccupiedRect().ContractedBy(1).Contains(c))
				{
					return 0f;
				}
				List<Thing> thingList2 = c.GetThingList(map);
				for (int j = 0; j < thingList2.Count; j++)
				{
					if (thingList2[j].def.category == ThingCategory.Filth && !thingList2[j].def.filth.allowsFire)
					{
						return 0f;
					}
				}
			}
			return num;
		}

		public static bool TryStartFireIn(IntVec3 c, Map map, float fireSize)
		{
			if (AmaterasuFireUtility.ChanceToStartFireIn(c, map) <= 0f)
			{
				return false;
			}
			AmaterasuFire expr_20 = (AmaterasuFire)ThingMaker.MakeThing(ThingDefOfLocal.AmaterasuFire, null);
			expr_20.fireSize = fireSize;
			GenSpawn.Spawn(expr_20, c, map, Rot4.North, WipeMode.Vanish, false);
			return true;
		}

		public static float ChanceToAttachFireFromEvent(Thing t)
		{
			return AmaterasuFireUtility.ChanceToAttachFireCumulative(t, 60f);
		}

		public static float ChanceToAttachFireCumulative(Thing t, float freqInTicks)
		{
			if (!t.CanEverAttachFire())
			{
				return 0f;
			}
			if (t.HasAttachment(ThingDefOf.Fire))
			{
				return 0f;
			}
			float num = AmaterasuFireUtility.ChanceToCatchFirePerSecondForPawnFromFlammability.Evaluate(t.GetStatValue(StatDefOf.Flammability, true));
			return 1f - Mathf.Pow(1f - num, freqInTicks / 60f);
		}

		public static void TryAttachFire(this Thing t, float fireSize)
		{
			if (!t.CanEverAttachFire())
			{
				return;
			}
			if (t.HasAttachment(ThingDefOf.Fire))
			{
				return;
			}
			AmaterasuFire expr_27 = (AmaterasuFire)ThingMaker.MakeThing(ThingDefOfLocal.AmaterasuFire, null);
			expr_27.fireSize = fireSize;
			expr_27.AttachTo(t);
			GenSpawn.Spawn(expr_27, t.Position, t.Map, Rot4.North, WipeMode.Vanish, false);
			Pawn pawn = t as Pawn;
			if (pawn != null)
			{
				pawn.jobs.StopAll(false, true);
				pawn.records.Increment(RecordDefOf.TimesOnFire);
			}
		}

		public static bool IsBurning(this TargetInfo t)
		{
			if (t.HasThing)
			{
				return t.Thing.IsBurning();
			}
			return t.Cell.ContainsStaticFire(t.Map);
		}

		public static bool IsBurning(this Thing t)
		{
			if (t.Destroyed || !t.Spawned)
			{
				return false;
			}
			if (!(t.def.size == IntVec2.One))
			{
				using (CellRect.Enumerator enumerator = t.OccupiedRect().GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.ContainsStaticFire(t.Map))
						{
							return true;
						}
					}
				}
				return false;
			}
			if (t is Pawn)
			{
				return t.HasAttachment(ThingDefOf.Fire);
			}
			return t.Position.ContainsStaticFire(t.Map);
		}

		public static bool ContainsStaticFire(this IntVec3 c, Map map)
		{
			List<Thing> list = map.thingGrid.ThingsListAt(c);
			for (int i = 0; i < list.Count; i++)
			{
				AmaterasuFire fire = list[i] as AmaterasuFire;
				if (fire != null && fire.parent == null)
				{
					return true;
				}
			}
			return false;
		}

		public static bool ContainsTrap(this IntVec3 c, Map map)
		{
			Building edifice = c.GetEdifice(map);
			return edifice != null && edifice is Building_Trap;
		}

		public static bool Flammable(this TerrainDef terrain)
		{
			return terrain.GetStatValueAbstract(StatDefOf.Flammability, null) > 0.01f;
		}

		public static bool TerrainFlammableNow(this IntVec3 c, Map map)
		{
			if (!c.GetTerrain(map).Flammable())
			{
				return false;
			}
			List<Thing> thingList = c.GetThingList(map);
			for (int i = 0; i < thingList.Count; i++)
			{
				if (thingList[i].FireBulwark)
				{
					return false;
				}
			}
			return true;
		}
	}
}
