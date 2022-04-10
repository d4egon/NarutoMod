// Decompiled with JetBrains decompiler
// Type: NarutoMod.Verbs.Verb_TeleportToCaster
// Assembly: NarutoMod, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A24FD7BF-E4B7-40C4-8848-97E48E1CC6B6
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\RimWorld\Mods\SovereignNarutoMod\Assemblies\NarutoMod.dll

using RimWorld;

namespace NarutoMod.Verbs
{
    public class NM_Verb_TeleportToCaster : NM_Verb_AbilityHediff
    {
        public override void WarmupComplete()
        {
            base.WarmupComplete();
            AddEffecterToMaintain(EffecterDefOf.Skip_Entry.Spawn(currentTarget.Pawn.Position, CasterPawn.Map, 1f), currentTarget.Cell, 120, currentTarget.Pawn);
            AddEffecterToMaintain(EffecterDefOf.Skip_Entry.Spawn(CasterPawn.Position, CasterPawn.Map, 1f), CasterPawn.Position, 120, CasterPawn);
            currentTarget.Pawn.Position = CasterPawn.Position;
            currentTarget.Pawn.stances.stunner.StunFor(120, CasterPawn, false, false);
            currentTarget.Pawn.Notify_Teleported(true, true);
        }
    }
}
