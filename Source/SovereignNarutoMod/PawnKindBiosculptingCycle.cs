using RimWorld;
using System.Collections.Generic;
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

            for (int i = 0; i > 1; i++)
            {
                if (newPawn.health != null)
                {
                    newPawn.health = occupant.health;
                    newPawn.health.AddHediff(Props.hediffDef, null, null, null);
                }
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