using System;
using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse;
using VFECore;


namespace NarutoMod.Effects
{
    public class Animated_ExpandableProjectile : ExpandableProjectile
    {
        public override void DoDamage(IntVec3 pos)
        {
            base.DoDamage(pos);
            try
            {
                if (pos != this.launcher.Position && this.launcher.Map != null && GenGrid.InBounds(pos, this.launcher.Map))
                {
                    List<Thing> list = this.launcher.Map.thingGrid.ThingsListAt(pos);
                    for (int i = list.Count - 1; i >= 0; i--)
                    {
                        if (this.IsDamagable(list[i]))
                        {
                            if (!(from x in list
                                  where x.def == ThingDefOf.Fire
                                  select x).Any<Thing>())
                            {
                                this.customImpact = true;
                            }
                            base.Impact(list[i]);
                            this.customImpact = false;
                        }
                    }
                }
            }
            catch 
            { 
            }
        }

        public override bool IsDamagable(Thing t)
        {
            return base.IsDamagable(t) && t.def != ThingDefOf.Fire;
        }
    }

}
