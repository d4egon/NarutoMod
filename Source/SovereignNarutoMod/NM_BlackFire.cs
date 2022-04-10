﻿using System.Collections.Generic;
using UnityEngine;
using Verse;
using Verse.Sound;
using RimWorld;

namespace NarutoMod
{
	public class NM_BlackFire : AttachableThing, ISizeReporter
		{
			private int ticksSinceSpawn;

			public float fireSize = 0.1f;

			private int ticksSinceSpread;

			private float flammabilityMax = 0.5f;

			private int ticksUntilSmoke;

			private Sustainer sustainer;

			private static List<Thing> flammableList = new List<Thing>();

			private static int fireCount;

			private static int lastFireCountUpdateTick;

			public const float MinFireSize = 0.1f;

			private const float MinSizeForSpark = 1f;

			private const float TicksBetweenSparksBase = 150f;

			private const float TicksBetweenSparksReductionPerFireSize = 40f;

			private const float MinTicksBetweenSparks = 75f;

			private const float MinFireSizeToEmitSpark = 1f;

			public const float MaxFireSize = 1.75f;

			private const int TicksToBurnFloor = 7500;

			private const int ComplexCalcsInterval = 150;

			private const float CellIgniteChancePerTickPerSize = 0.01f;

			private const float MinSizeForIgniteMovables = 0.4f;

			private const float FireBaseGrowthPerTick = 0.00055f;

			private static readonly IntRange SmokeIntervalRange = new IntRange(130, 200);

			private const int SmokeIntervalRandomAddon = 10;

			private const float BaseSkyExtinguishChance = 0.04f;

			private const int BaseSkyExtinguishDamage = 10;

			private const float HeatPerFireSizePerInterval = 160f;

			private const float HeatFactorWhenDoorPresent = 0.15f;

			private const float SnowClearRadiusPerFireSize = 3f;

			private const float SnowClearDepthFactor = 0.1f;

			private const int FireCountParticlesOff = 15;

			public int TicksSinceSpawn
			{
				get
				{
					return this.ticksSinceSpawn;
				}
			}

			public override string Label
			{
				get
				{
					if (this.parent != null)
					{
						return "FireOn".Translate(this.parent.LabelCap, this.parent);
					}
					return this.def.label;
				}
			}

			public override string InspectStringAddon
			{
				get
				{
					return "Burning".Translate() + " (" + "FireSizeLower".Translate((this.fireSize * 100f).ToString("F0")) + ")";
				}
			}

			private float SpreadInterval
			{
				get
				{
					float num = 150f - (this.fireSize - 1f) * 40f;
					if (num < 75f)
					{
						num = 75f;
					}
					return num;
				}
			}

			public override void ExposeData()
			{
				base.ExposeData();
				Scribe_Values.Look<int>(ref this.ticksSinceSpawn, "ticksSinceSpawn", 0, false);
				Scribe_Values.Look<float>(ref this.fireSize, "fireSize", 0f, false);
			}

			public override void SpawnSetup(Map map, bool respawningAfterLoad)
			{
				base.SpawnSetup(map, respawningAfterLoad);
				this.RecalcPathsOnAndAroundMe(map);
				LessonAutoActivator.TeachOpportunity(ConceptDefOf.HomeArea, this, OpportunityType.Important);
				this.ticksSinceSpread = (int)(this.SpreadInterval * Rand.Value);
			}

			public float CurrentSize()
			{
				return this.fireSize;
			}

			public override void DeSpawn(DestroyMode mode = DestroyMode.Vanish)
			{
				if (this.sustainer != null)
				{
					if (this.sustainer.externalParams.sizeAggregator == null)
					{
						this.sustainer.externalParams.sizeAggregator = new SoundSizeAggregator();
					}
					this.sustainer.externalParams.sizeAggregator.RemoveReporter(this);
				}
				Map map = base.Map;
				base.DeSpawn(mode);
				this.RecalcPathsOnAndAroundMe(map);
			}

			private void RecalcPathsOnAndAroundMe(Map map)
			{
				IntVec3[] adjacentCellsAndInside = GenAdj.AdjacentCellsAndInside;
				for (int i = 0; i < adjacentCellsAndInside.Length; i++)
				{
					IntVec3 c = base.Position + adjacentCellsAndInside[i];
					if (c.InBounds(map))
					{
						map.pathing.RecalculatePerceivedPathCostAt(c);
					}
				}
			}

			public override void AttachTo(Thing parent)
			{
				base.AttachTo(parent);
				Pawn pawn = parent as Pawn;
				if (pawn != null)
				{
					TaleRecorder.RecordTale(TaleDefOf.WasOnFire, new object[]
					{
					pawn
					});
				}
			}

			public override void Tick()
			{
				this.ticksSinceSpawn++;
				if (lastFireCountUpdateTick != Find.TickManager.TicksGame)
				{
				fireCount = base.Map.listerThings.ThingsOfDef(this.def).Count;
				lastFireCountUpdateTick = Find.TickManager.TicksGame;
				}
				if (this.sustainer != null)
				{
					this.sustainer.Maintain();
				}
				else if (!base.Position.Fogged(base.Map))
				{
					SoundInfo info = SoundInfo.InMap(new TargetInfo(base.Position, base.Map, false), MaintenanceType.PerTick);
					this.sustainer = SustainerAggregatorUtility.AggregateOrSpawnSustainerFor(this, SoundDefOf.FireBurning, info);
				}
				this.ticksUntilSmoke--;
				if (this.ticksUntilSmoke <= 0)
				{
					this.SpawnSmokeParticles();
				}
				if (NM_BlackFire.fireCount < 15 && this.fireSize > 0.7f && Rand.Value < this.fireSize * 0.01f)
				{
					FleckMaker.ThrowMicroSparks(this.DrawPos, base.Map);
				}
				if (this.fireSize > 1f)
				{
					this.ticksSinceSpread++;
					if ((float)this.ticksSinceSpread >= this.SpreadInterval)
					{
						this.TrySpread();
						this.ticksSinceSpread = 0;
					}
				}
				if (this.IsHashIntervalTick(150))
				{
					this.DoComplexCalcs();
				}
				if (this.ticksSinceSpawn >= 7500)
				{
					this.TryBurnFloor();
				}
			}

			private void SpawnSmokeParticles()
			{
				if (fireCount < 15)
				{
					FleckMaker.ThrowSmoke(this.DrawPos, base.Map, this.fireSize);
				}
				if (this.fireSize > 0.5f && this.parent == null)
				{
					FleckMaker.ThrowFireGlow(base.Position.ToVector3Shifted(), base.Map, this.fireSize);
				}
				float num = this.fireSize / 2f;
				if (num > 1f)
				{
					num = 1f;
				}
				num = 1f - num;
				this.ticksUntilSmoke = SmokeIntervalRange.Lerped(num) + (int)(10f * Rand.Value);
			}

			private void DoComplexCalcs()
			{
            bool flag = false;
            flammableList.Clear();
				this.flammabilityMax = 0f;
            if (!base.Position.GetTerrain(base.Map).extinguishesFire)
				{
					if (this.parent == null)
					{
						if (base.Position.TerrainFlammableNow(base.Map))
						{
							this.flammabilityMax = base.Position.GetTerrain(base.Map).GetStatValueAbstract(StatDefOf.Flammability, null);
						}
						List<Thing> list = base.Map.thingGrid.ThingsListAt(base.Position);
						for (int i = 0; i < list.Count; i++)
						{
							Thing expr_97 = list[i];
							if (expr_97 is Building_Door)
							{
								flag = true;
							}
							float statValue = expr_97.GetStatValue(StatDefOf.Flammability, true);
							if (statValue >= 0.01f)
							{
							flammableList.Add(list[i]);
								if (statValue > this.flammabilityMax)
								{
									this.flammabilityMax = statValue;
								}
								if (this.parent == null && this.fireSize > 0.4f && list[i].def.category == ThingCategory.Pawn && Rand.Chance(NM_BlackFireUtility.ChanceToAttachFireCumulative(list[i], 150f)))
								{
									list[i].TryAttachFire(this.fireSize * 0.2f);
								}
							}
						}
					}
					else
					{
					flammableList.Add(this.parent);
						this.flammabilityMax = this.parent.GetStatValue(StatDefOf.Flammability, true);
					}
				}
				if (this.flammabilityMax < 0.01f)
				{
					this.Destroy(DestroyMode.Vanish);
					return;
				}
				Thing thing;
				if (this.parent != null)
				{
					thing = this.parent;
				}
				else if (flammableList.Count > 0)
				{
					thing = flammableList.RandomElement<Thing>();
				}
				else
				{
					thing = null;
				}
				if (thing != null && (this.fireSize >= 0.4f || thing == this.parent || thing.def.category != ThingCategory.Pawn))
				{
					this.DoFireDamage(thing);
				}
				if (base.Spawned)
				{
					float num = this.fireSize * 160f;
					if (flag)
					{
						num *= 0.15f;
					}
					GenTemperature.PushHeat(base.Position, base.Map, num);
					if (Rand.Value < 0.4f)
					{
						float radius = this.fireSize * 3f;
						SnowUtility.AddSnowRadial(base.Position, base.Map, radius, -(this.fireSize * 0.1f));
					}
					this.fireSize += 0.00055f * this.flammabilityMax * 150f;
					if (this.fireSize > 1.75f)
					{
						this.fireSize = 1.75f;
					}
					if (base.Map.weatherManager.RainRate > 0.01f && this.VulnerableToRain() && Rand.Value < 6f)
					{
						base.TakeDamage(new DamageInfo(DamageDefOf.Extinguish, 10f, 0f, -1f, null, null, null, DamageInfo.SourceCategory.ThingOrUnknown, null, true, true));
					}
				}
			}

			private void TryBurnFloor()
			{
				if (this.parent != null || !base.Spawned)
				{
					return;
				}
				if (base.Position.TerrainFlammableNow(base.Map))
				{
					base.Map.terrainGrid.Notify_TerrainBurned(base.Position);
				}
			}

			private bool VulnerableToRain()
			{
				if (!base.Spawned)
				{
					return false;
				}
				RoofDef roofDef = base.Map.roofGrid.RoofAt(base.Position);
				if (roofDef == null)
				{
					return true;
				}
				if (roofDef.isThickRoof)
				{
					return false;
				}
				Thing edifice = base.Position.GetEdifice(base.Map);
				return edifice != null && edifice.def.holdsRoof;
			}

			private void DoFireDamage(Thing targ)
			{
				int num = GenMath.RoundRandom(Mathf.Clamp(0.0125f + 0.0036f * this.fireSize, 0.0125f, 0.05f) * 150f);
				if (num < 1)
				{
					num = 1;
				}
				Pawn pawn = targ as Pawn;
				if (pawn != null)
				{
					BattleLogEntry_DamageTaken battleLogEntry_DamageTaken = new BattleLogEntry_DamageTaken(pawn, RulePackDefOf.DamageEvent_Fire, null);
					Find.BattleLog.Add(battleLogEntry_DamageTaken);
					DamageInfo dinfo = new DamageInfo(DamageDefOf.Flame, (float)num, 0f, -1f, this, null, null, DamageInfo.SourceCategory.ThingOrUnknown, null, true, true);
					dinfo.SetBodyRegion(BodyPartHeight.Undefined, BodyPartDepth.Outside);
					targ.TakeDamage(dinfo).AssociateWithLog(battleLogEntry_DamageTaken);
					Apparel apparel;
					if (pawn.apparel != null && pawn.apparel.WornApparel.TryRandomElement(out apparel))
					{
						apparel.TakeDamage(new DamageInfo(DamageDefOf.Flame, (float)num, 0f, -1f, this, null, null, DamageInfo.SourceCategory.ThingOrUnknown, null, true, true));
						return;
					}
				}
				else
				{
					targ.TakeDamage(new DamageInfo(DamageDefOf.Flame, (float)num, 0f, -1f, this, null, null, DamageInfo.SourceCategory.ThingOrUnknown, null, true, true));
				}
			}

			protected void TrySpread()
			{
				IntVec3 intVec = base.Position;
				bool flag;
				if (Rand.Chance(0.8f))
				{
					intVec = base.Position + GenRadial.ManualRadialPattern[Rand.RangeInclusive(1, 8)];
					flag = true;
				}
				else
				{
					intVec = base.Position + GenRadial.ManualRadialPattern[Rand.RangeInclusive(10, 20)];
					flag = false;
				}
				if (!intVec.InBounds(base.Map))
				{
					return;
				}
				if (Rand.Chance(NM_BlackFireUtility.ChanceToStartFireIn(intVec, base.Map)))
				{
					if (!flag)
					{
						CellRect startRect = CellRect.SingleCell(base.Position);
						CellRect endRect = CellRect.SingleCell(intVec);
						if (!GenSight.LineOfSight(base.Position, intVec, base.Map, startRect, endRect, null))
						{
							return;
						}
						((NM_BlackSpark)GenSpawn.Spawn(ThingDefOf.Spark, base.Position, base.Map, WipeMode.Vanish)).Launch(this, intVec, intVec, ProjectileHitFlags.All, false, null);
						return;
					}
					else
					{
						NM_BlackFireUtility.TryStartFireIn(intVec, base.Map, 0.1f);
					}
				}
			}
		}
	}
