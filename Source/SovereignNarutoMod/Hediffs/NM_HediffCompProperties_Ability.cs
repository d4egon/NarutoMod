using NarutoMod.Verbs;
using System.Collections.Generic;
using Verse;

namespace NarutoMod.Hediffs
{
    public class NM_HediffCompProperties_Ability : HediffCompProperties
    {
        public List<NM_VerbProperties_Ability> verbProps = new List<NM_VerbProperties_Ability>();

        public NM_HediffCompProperties_Ability() => compClass = typeof(NM_HediffComp_Ability);
    }
}
