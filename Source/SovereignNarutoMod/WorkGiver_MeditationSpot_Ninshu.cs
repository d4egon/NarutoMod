using System;
using RimWorld;
using UnityEngine;
using Verse;
using Verse.AI;

namespace NarutoMod
{
    public class WorkGiver_MeditationSpot_Ninshu : WorkGiver_Scanner
    {
        public override ThingRequest PotentialWorkThingRequest => ThingRequest.ForDef(DefDatabase<ThingDef>.GetNamed("NM_MeditationSpot_Ninshu"));

        public override PathEndMode PathEndMode => PathEndMode.Touch;

        public override bool HasJobOnThing(Pawn pawn, Thing t, bool forced = false)
        {
            if (t.IsForbidden(pawn) || !pawn.CanReserve(t, ignoreOtherReservations: forced) || pawn.Map.designationManager.DesignationOn(t, DesignationDefOf.Deconstruct) != null)
                return false;
            CompBiosculpterPod comp = t.TryGetComp<CompBiosculpterPod>();
            if (comp == null || !comp.PowerOn || comp.State != BiosculpterPodState.LoadingNutrition || !forced && !comp.autoLoadNutrition || t.IsBurning() || comp.RequiredNutritionRemaining <= 0.0)
                return false;
            if (FindNutrition(pawn, comp).Thing != null)
                return true;
            JobFailReason.Is("NoFood".Translate());
            return false;
        }

        public override Job JobOnThing(Pawn pawn, Thing t, bool forced = false)
        {
            CompBiosculpterPod comp = t.TryGetComp<CompBiosculpterPod>();
            if (comp == null)
                return null;
            if (comp.RequiredNutritionRemaining > 0.0)
            {
                ThingCount nutrition = FindNutrition(pawn, comp);
                if (nutrition.Thing != null)
                {
                    Job containerJob = HaulAIUtility.HaulToContainerJob(pawn, nutrition.Thing, t);
                    containerJob.count = Mathf.Min(containerJob.count, nutrition.Count);
                    return containerJob;
                }
            }
            return null;
        }

        private ThingCount FindNutrition(Pawn pawn, CompBiosculpterPod pod)
        {
            Thing thing = GenClosest.ClosestThingReachable(pawn.Position, pawn.Map, ThingRequest.ForGroup(ThingRequestGroup.FoodSourceNotPlantOrTree), PathEndMode.ClosestTouch, TraverseParms.For(pawn), validator: new Predicate<Thing>(Validator));
            if (thing == null)
                return new ThingCount();
            int b = Mathf.CeilToInt(pod.RequiredNutritionRemaining / thing.GetStatValue(StatDefOf.Nutrition));
            return new ThingCount(thing, Mathf.Min(thing.stackCount, b));

            bool Validator(Thing x) => !x.IsForbidden(pawn) && pawn.CanReserve(x) && pod.CanAcceptNutrition(x);
        }
    }
}