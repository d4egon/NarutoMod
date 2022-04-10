// Decompiled with JetBrains decompiler
// Type: NarutoMod.Verbs.Verb_ThrowMeteor
// Assembly: NarutoMod, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A24FD7BF-E4B7-40C4-8848-97E48E1CC6B6
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\RimWorld\Mods\SovereignNarutoMod\Assemblies\NarutoMod.dll

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
