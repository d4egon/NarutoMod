using System;
using UnityEngine;
using Verse;
using RimWorld;

namespace SovereignNarutoMod
{
	public static class SNM_FleckMaker
	{
		public static FleckCreationData GetDataStatic(Vector3 loc, Map map, FleckDef fleckDef, float scale = 1f)
		{
			return new FleckCreationData
			{
				def = fleckDef,
				spawnPosition = loc,
				scale = scale
			};
		}

		public static void Static(IntVec3 cell, Map map, FleckDef fleckDef, float scale = 1f)
		{
            RimWorld.FleckMaker.Static(cell.ToVector3Shifted(), map, fleckDef, scale);
		}

		public static void Static(Vector3 loc, Map map, FleckDef fleckDef, float scale = 1f)
		{
			map.flecks.CreateFleck(RimWorld.FleckMaker.GetDataStatic(loc, map, fleckDef, scale));
		}

		public static FleckCreationData GetDataThrowMetaIcon(IntVec3 cell, Map map, FleckDef fleckDef, float velocitySpeed = 0.42f)
		{
			return new FleckCreationData
			{
				def = fleckDef,
				spawnPosition = cell.ToVector3Shifted() + new Vector3(0.35f, 0f, 0.35f) + new Vector3(Rand.Value, 0f, Rand.Value) * 0.1f,
				velocityAngle = (float)Rand.Range(30, 60),
				velocitySpeed = velocitySpeed,
				rotationRate = Rand.Range(-3f, 3f),
				scale = 0.7f
			};
		}
		public static void ThrowAmaterasuFireGlow(Vector3 c, Map map, float size)
		{
			if (!c.ShouldSpawnMotesAt(map))
			{
				return;
			}
			Vector3 vector = c + size * new Vector3(Rand.Value - 0.5f, 0f, Rand.Value - 0.5f);
			if (!vector.InBounds(map))
			{
				return;
			}
            FleckCreationData dataStatic = RimWorld.FleckMaker.GetDataStatic(vector, map, SovereignNarutoMod.FleckDefOf.AmaterasuFireGlow, Rand.Range(4f, 6f) * size);
			dataStatic.rotationRate = Rand.Range(-3f, 3f);
			dataStatic.velocityAngle = (float)Rand.Range(0, 360);
			dataStatic.velocitySpeed = 0.12f;
			map.flecks.CreateFleck(dataStatic);
		}
		public static void ThrowAmaterasuMicroSparks(Vector3 loc, Map map)
		{
			if (!loc.ShouldSpawnMotesAt(map))
			{
				return;
			}
			loc -= new Vector3(0.5f, 0f, 0.5f);
			loc += new Vector3(Rand.Value, 0f, Rand.Value);
            FleckCreationData dataStatic = RimWorld.FleckMaker.GetDataStatic(loc, map, SovereignNarutoMod.FleckDefOf.AmaterasuMicroSparks, Rand.Range(0.8f, 1.2f));
			dataStatic.rotationRate = Rand.Range(-12f, 12f);
			dataStatic.velocityAngle = (float)Rand.Range(35, 45);
			dataStatic.velocitySpeed = 1.2f;
			map.flecks.CreateFleck(dataStatic);
		}
	}
}
