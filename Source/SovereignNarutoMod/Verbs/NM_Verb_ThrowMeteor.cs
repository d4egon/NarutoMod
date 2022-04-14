using RimWorld;

namespace NarutoMod.Verbs
{
    public class NM_Verb_ThrowMeteor : NM_Verb_AbilityHediff
    {
        public override void WarmupComplete()
        {
            base.WarmupComplete();
            SkyfallerMaker.SpawnSkyfaller(ThingDefOf.MeteoriteIncoming, currentTarget.Cell, CasterPawn.Map);
            CasterPawn.stances.stunner.StunFor(45, CasterPawn, false, false);
        }
    }
}
