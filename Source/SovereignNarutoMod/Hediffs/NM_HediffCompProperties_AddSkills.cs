using RimWorld;
using Verse;

namespace NarutoMod.Hediffs
{
    public class NM_HediffCompProperties_AddSkills : HediffCompProperties
    {
        public SkillDef skillDef;
        public int levels;

        public NM_HediffCompProperties_AddSkills() => compClass = typeof(NM_HediffComp_AddSkills);
    }
}
