using System.Collections.Generic;
using Verse;
using RimWorld;
using VFECore;

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
        public ExpandableProjectile projectile;
        public float damageAmount;
        public float severity;
        public FloatRange conditionDaysRange;
        //public GameConditionDef gameConditionDef;
        public List<IntVec2> pattern;
        public bool ignoreRange;
        public PawnKindDef pawnKindToSpawn;
        public IntRange cooldownTicksRange;
        public int cooldownTicks;
        public bool sendLetterOnCooldownComplete;
        


        [Unsaved(false)]
        protected TaggedString cachedLabelCap = null;

        public virtual TaggedString LabelCap
        {
            get
            {
                if (this.label.NullOrEmpty())
                {
                    return null;
                }
                if (this.cachedLabelCap.NullOrEmpty())
                {
                    this.cachedLabelCap = this.label.CapitalizeFirst();
                }
                return this.cachedLabelCap;
            }
        }
    }
}
