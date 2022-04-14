using Verse;

namespace NarutoMod.Things
{
    public class NM_CompProperties_RaceComp : CompProperties
    {
        public float powerBase;
        public float powerGain;

        public NM_CompProperties_RaceComp(float powerBase, float powerGain)
        {
            compClass = typeof(NM_Comp_RaceComp);
            this.powerBase = powerBase;
            this.powerGain = powerGain;
        }
    }
}
