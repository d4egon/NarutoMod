using ExplosiveTrailsEffect;
using RimWorld;
using Verse;

namespace NarutoMod.Effects
{
    public class NM_Projectile_OilTrail : Projectile_Explosive
    {
        private int TicksforAppearence = 3;

        public override void Tick()
        {
            base.Tick();
            TicksforAppearence--;
            if (TicksforAppearence == 0 && Map != null)
            {
                SmokeThrowher.ThrowSmokeTrail(Position.ToVector3Shifted(), 0.4f, Map, "NM_Mote_OiltrailSoft");
                TicksforAppearence = 3;
            }
        }
    }
}