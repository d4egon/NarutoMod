// Decompiled with JetBrains decompiler
// Type: NarutoMod.Hediffs.HediffComp_AddSkills
// Assembly: NarutoMod, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A24FD7BF-E4B7-40C4-8848-97E48E1CC6B6
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\RimWorld\Mods\SovereignNarutoMod\Assemblies\NarutoMod.dll

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
