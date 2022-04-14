using RimWorld;
using UnityEngine;
using Verse;

namespace NarutoMod.Hediffs
{
    public class NM_HediffComp_AddSkills : HediffComp
    {
        public NM_HediffCompProperties_AddSkills Props => props as NM_HediffCompProperties_AddSkills;

        public override void CompPostPostAdd(DamageInfo? dinfo)
        {
            base.CompPostPostAdd(dinfo);
            SkillRecord skillRecord = parent.pawn.skills.skills.Find(x => x.def == Props.skillDef);
            if (skillRecord.TotallyDisabled)
                return;
            skillRecord.Level = Mathf.Clamp(skillRecord.Level + Props.levels, 0, 20);
        }

        public override void CompPostPostRemoved()
        {
            base.CompPostPostRemoved();
            SkillRecord skillRecord = parent.pawn.skills.skills.Find(x => x.def == Props.skillDef);
            if (skillRecord.TotallyDisabled)
                return;
            skillRecord.Level = Mathf.Clamp(skillRecord.Level - Props.levels, 0, 20);
        }
    }
}
