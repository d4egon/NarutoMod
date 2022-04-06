// Decompiled with JetBrains decompiler
// Type: NarutoMod.Verbs.VerbProperties_Ability
// Assembly: NarutoMod, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A24FD7BF-E4B7-40C4-8848-97E48E1CC6B6
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\RimWorld\Mods\SovereignNarutoMod\Assemblies\NarutoMod.dll

using System.Collections.Generic;
using Verse;

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
    }
}
