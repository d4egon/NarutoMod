using RimWorld;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using Verse;

namespace SovereignNarutoMod.Things
{
	public class Building_FruitTree : Plant
	{
		private float fuel;

		private float fruitProgres;

		private int TicksGame
		{
			get
			{
				return Find.get_TickManager().get_TicksGame();
			}
		}

		public float Fuel
		{
			get
			{
				return this.fuel;
			}
		}

		private float fuelPerPawn
		{
			get
			{
				return 100f;
			}
		}

		public float FuelMax
		{
			get
			{
				return 500f;
			}
		}

		private float fuelPerSecond
		{
			get
			{
				return -0.0333333f;
			}
		}

		public float FuelPerDay
		{
			get
			{
				return this.fuelPerSecond * 1000f;
			}
		}

		public float FruitProgres
		{
			get
			{
				return this.fruitProgres;
			}
		}

		public float FruitProgresMax
		{
			get
			{
				return 100f;
			}
		}

		private float fruitProgresPerSecond
		{
			get
			{
				return 0.006666666f;
			}
		}

		private int pawnsToFruit
		{
			get
			{
				return (int)(Math.Abs(this.fuelPerSecond) / this.fruitProgresPerSecond);
			}
		}

		public float FruitProgresPerDay
		{
			get
			{
				if (this.Fuel <= Mathf.Abs(this.fuelPerSecond))
				{
					return 0f;
				}
				return this.fruitProgresPerSecond * 1000f;
			}
		}

		public float FruitStartProgres
		{
			get
			{
				return 0.5f;
			}
		}

		public override void Tick()
		{
			base.Tick();
			if (this.TicksGame % 60 == 0 && this.get_Growth() > this.FruitStartProgres)
			{
				if (this.fuel > Mathf.Abs(this.fuelPerSecond))
				{
					this.fuel = Mathf.Clamp(this.fuel + this.fuelPerSecond, 0f, this.FuelMax);
					this.fruitProgres = Mathf.Clamp(this.fruitProgres + this.fruitProgresPerSecond, 0f, this.FruitProgresMax);
				}
				if (this.fruitProgres >= this.FruitProgresMax)
				{
					this.fruitProgres = 0f;
					GenSpawn.Spawn(ThingDefOfLocal.ThingDef_StrangeFruit, base.get_Position(), base.get_Map(), 0);
					for (int i = 0; i < this.pawnsToFruit; i++)
					{
						GenSpawn.Spawn(ThingDefOfLocal.ThingDef_PlaceholderMaterial, base.get_Position(), base.get_Map(), 0);
					}
				}
			}
		}

		public void Fill(Pawn pawn)
		{
			this.fuel = Mathf.Clamp(this.fuel + this.fuelPerPawn, 0f, this.FuelMax);
			pawn.Destroy(0);
		}

		public void Fill(Thing thing)
		{
			this.fuel = Mathf.Clamp(this.fuel + this.fuelPerPawn, 0f, this.FuelMax);
		}

		[IteratorStateMachine(typeof(Building_FruitTree.< GetFloatMenuOptions > d__29))]
		public override IEnumerable<FloatMenuOption> GetFloatMenuOptions(Pawn selPawn)
		{
			Building_FruitTree.< GetFloatMenuOptions > d__29 expr_07 = new Building_FruitTree.< GetFloatMenuOptions > d__29(-2);
			expr_07.<> 4__this = this;
			expr_07.<> 3__selPawn = selPawn;
			return expr_07;
		}

		[IteratorStateMachine(typeof(Building_FruitTree.< GetGizmos > d__30))]
		public override IEnumerable<Gizmo> GetGizmos()
		{
			Building_FruitTree.< GetGizmos > d__30 expr_07 = new Building_FruitTree.< GetGizmos > d__30(-2);
			expr_07.<> 4__this = this;
			return expr_07;
		}
	}
}
