using NarutoMod.Verbs;
using System;
using Verse;

namespace NarutoMod.Gizmos
{
    public class NM_Command_HediffAbility : Command_VerbTarget
    {
        public NM_Verb_AbilityHediff Verb
        {
            get
            {
                return verb as NM_Verb_AbilityHediff;
            }
        }

        public virtual string GetTopRightLabel()
        {
            return Math.Round(Verb.Props.powerCost, 1).ToString() + " ";
        }
    }
}
