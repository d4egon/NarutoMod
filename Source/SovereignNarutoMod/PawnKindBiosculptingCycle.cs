using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;

namespace NarutoMod
{
    public class PawnKindBiosculptingCycle : CompBiosculpterPod_Cycle
    {


        public new PawnKindBiosculptingCycle_Properties Props
        {
            get
            {
                return (PawnKindBiosculptingCycle_Properties)this.props;
            }
        }

        public override void CycleCompleted(Pawn occupant)
        {
            var newPawn = (Pawn)GenSpawn.Spawn(
                PawnGenerator.GeneratePawn(new PawnGenerationRequest(Props.pawnKind, parent.Faction, newborn: true, allowAddictions: false, fixedGender: occupant.gender)),
                parent.InteractionCell,
                parent.Map
            );

            newPawn.Name = occupant.Name;
            newPawn.relations = occupant.relations;
            newPawn.ageTracker.AgeBiologicalTicks = occupant.ageTracker.AgeBiologicalTicks;
            newPawn.ageTracker.AgeChronologicalTicks = occupant.ageTracker.AgeChronologicalTicks;
            newPawn.ageTracker.DebugMakeOlder(0);

            if (newPawn.story != null)
            {
                newPawn.story.childhood = occupant.story.childhood;
                newPawn.story.adulthood = occupant.story.adulthood;
                newPawn.story.traits = occupant.story.traits;
                newPawn.story.favoriteColor = occupant.story.favoriteColor;
                newPawn.story.hairColor = occupant.story.hairColor;
                newPawn.story.hairDef = occupant.story.hairDef;
                newPawn.story.bodyType = occupant.story.bodyType;
            }

            if (newPawn.skills != null)
            {
                newPawn.skills = occupant.skills;
            }

            if (newPawn.abilities != null)
            {
                newPawn.abilities = occupant.abilities;
            }

            if (newPawn.ideo != null)
            {
                newPawn.ideo = occupant.ideo;
            }

            if (newPawn.health != null)
            {
                newPawn.health = occupant.health;
            }
            
            if (occupant.health.hediffSet.HasHediff(NM_HediffDefOf.HediffDef_Ninshu) != true && occupant.health.hediffSet.HasHediff(NM_HediffDefOf.Prostheses_HediffDef_NM_Ninshu_Monkey) != true && occupant.health.hediffSet.HasHediff(NM_HediffDefOf.Prostheses_HediffDef_NM_Ninshu_Shark) != true && occupant.health.hediffSet.HasHediff(NM_HediffDefOf.Prostheses_HediffDef_NM_Ninshu_Snake) != true && occupant.health.hediffSet.HasHediff(NM_HediffDefOf.Prostheses_HediffDef_NM_Ninshu_Toad) != true)
            {

                BodyPartRecord part = newPawn.RaceProps.body.GetPartsWithDef(this.Props.bodyPart).FirstOrDefault<BodyPartRecord>();
                newPawn.health.AddHediff(HediffDef.Named(this.Props.hediffname), part, null, null);
                newPawn.health.hediffSet.GetFirstHediffOfDef(HediffDef.Named(this.Props.hediffname), false).Severity += this.Props.hediffseverity;
            }

            if (newPawn.style != null)
            {
                newPawn.style = occupant.style;
            }
    occupant.Destroy();
            Find.ColonistBar.MarkColonistsDirty();

            parent.TryGetComp<CompBiosculpterPod>()?.SetBiotuned(null);
}
    }
}