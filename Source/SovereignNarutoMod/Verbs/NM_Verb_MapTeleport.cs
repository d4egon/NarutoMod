using NarutoMod.GUI;
using Verse;

namespace NarutoMod.Verbs
{
    public class NM_Verb_MapTeleport : NM_Verb_AbilityHediff
    {
        public override void WarmupComplete()
        {
            base.WarmupComplete();
            Find.WindowStack.Add(new NM_Window_MapTeleport(CasterPawn));
        }
    }

}
