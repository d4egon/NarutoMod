using NarutoMod.Things;
using Verse;

namespace NarutoMod.Hediffs
{

    public class NM_HediffComp_Ability : HediffComp
    {

        public NM_HediffCompProperties_Ability Props => props as NM_HediffCompProperties_Ability;


        private NM_Comp_RaceComp Comp_Race => parent.pawn.GetComp<NM_Comp_RaceComp>();


        public override void CompPostPostAdd(DamageInfo? dinfo)
        {
            base.CompPostPostAdd(dinfo);
            Comp_Race.InitVerbsFromZero();
        }

        public override void CompPostPostRemoved()
        {
            base.CompPostPostRemoved();
            Comp_Race.InitVerbsFromZero();
        }
    }
}
