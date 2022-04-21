using ExplosiveTrailsEffect;
using RimWorld;
using Verse;

namespace NarutoMod.Effects
{
    public class NM_Projectile_WaterTrail : Projectile_Explosive
    {
        private int TicksforAppearence = 2;

        public override void Tick()
        {
            base.Tick();
            TicksforAppearence--;
            if (TicksforAppearence == 0 && Map != null)
            {
                SmokeThrowher.ThrowSmokeTrail(Position.ToVector3Shifted(), 0.2f, Map, "NM_Mote_Watertrail");
                TicksforAppearence = 2;
            }
        }
    }
}