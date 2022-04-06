using System.Collections.Generic;
using System.Linq;
using Verse;
using RimWorld;

namespace NarutoMod.Verbs
{
    public class NM_Verb_WoodRelease_Wall : NM_Verb_AbilityHediff
    {
        public override void WarmupComplete()
        {
            base.WarmupComplete();
            Map map = CasterPawn.Map;
            List<Thing> thingList = new List<Thing>();
            thingList.AddRange(AffectedCells(CurrentTarget, map).SelectMany(c => ((IEnumerable<Thing>)GridsUtility.GetThingList(c, map)).Where(t => t.def.category == ThingCategory.Item)));
            foreach (Entity entity in thingList)
                entity.DeSpawn(0);
            foreach (IntVec3 affectedCell in AffectedCells(CurrentTarget, map))
                GenSpawn.Spawn(NM_ThingDefOf.ThingDef_RaisedWood, affectedCell, map, 0);
            foreach (Thing thing in thingList)
            {
                IntVec3 intVec3_1 = IntVec3.Invalid;
                for (int index = 0; index < 9; ++index)
                {
                    IntVec3 intVec3_2 = thing.Position + GenRadial.RadialPattern[index];
                    if (GenGrid.InBounds(intVec3_2, map) && GenGrid.Walkable(intVec3_2, map) && map.thingGrid.ThingsListAtFast(intVec3_2).Count <= 0)
                    {
                        intVec3_1 = intVec3_2;
                        break;
                    }
                }
                if (intVec3_1 != IntVec3.Invalid)
                    GenSpawn.Spawn(thing, intVec3_1, map, 0);
                else
                    GenPlace.TryPlaceThing(thing, thing.Position, map, (ThingPlaceMode)1, null, null, new Rot4());
            }
            AddEffecterToMaintain(EffecterDefOf.DryadEmergeFromCocoon.Spawn(currentTarget.Pawn.Position, CasterPawn.Map, 1f), currentTarget.Cell, 120, currentTarget.Pawn);
        }
        private IEnumerable<IntVec3> AffectedCells(LocalTargetInfo target, Map map)
        {
            foreach (IntVec2 intVec2 in Props.pattern)
            {
                IntVec3 intVec3 = target.Cell + new IntVec3(intVec2.x, 0, intVec2.z);
                if (GenGrid.InBounds(intVec3, map))
                    yield return intVec3;
            }
        }
    }
}
