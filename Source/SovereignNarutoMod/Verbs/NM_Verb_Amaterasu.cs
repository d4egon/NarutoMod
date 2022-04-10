// Decompiled with JetBrains decompiler
// Type: NarutoMod.Verbs.Verb_Amaterasu
// Assembly: NarutoMod, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A24FD7BF-E4B7-40C4-8848-97E48E1CC6B6
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\RimWorld\Mods\SovereignNarutoMod\Assemblies\NarutoMod.dll

using RimWorld;
using Verse;

namespace NarutoMod.Verbs
{
    public class NM_Verb_Amaterasu : NM_Verb_AbilityHediff
    {
        public override bool ValidateTarget(LocalTargetInfo target, bool showMessages = true)
        {
            if (target == null || target.Pawn == null)
                return false;
            if (target.Pawn.FlammableNow)
                return base.ValidateTarget(target, showMessages);
            Messages.Message(Translator.Translate("NarutoMod.Messages.TargetInflammable"), MessageTypeDefOf.RejectInput, false);
            return false;
        }

        public override void WarmupComplete()
        {
            NM_BlackFireUtility.TryAttachFire(base.currentTarget.Pawn, 1f);
            base.WarmupComplete();
            CasterPawn.stances.stunner.StunFor(60, CasterPawn, false, false);
        }
    }
}
