using System.Collections.Generic;
using RimWorld;
using Verse;

namespace NarutoMod
{
    public class PawnKindBiosculptingCycle_Properties : CompProperties_BiosculpterPod_BaseCycle
    {
        public PawnKindDef pawnKind;
        public HediffDef hediffDef;
        public BodyPartRecord bodyPart;
    }
}