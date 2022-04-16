using System.Collections.Generic;
using Verse;
using RimWorld;

namespace NarutoMod.Verbs
{
    public class NM_VerbProperties_Ability : VerbProperties
    {
        public float powerCost;
        [MustTranslate]
        public string description;
        public HediffDef hediffDef;
        public FleckDef fleckDef;
        public ThingDef moteDef;
        public float damageAmount;
        public float severity;
        public FloatRange conditionDaysRange;
        //public GameConditionDef gameConditionDef;
        public List<IntVec2> pattern;
        public bool ignoreRange;
        public PawnKindDef pawnKindToSpawn;
    }
}
