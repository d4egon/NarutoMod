using NarutoMod.Things;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;

namespace NarutoMod.Patches
{
    [StaticConstructorOnStartup]
    public static class NM_HumanlikeRacesPatch
    {
        static NM_HumanlikeRacesPatch()
        {
            List<ThingDef> list = ((IEnumerable<ThingDef>)DefDatabase<ThingDef>.AllDefsListForReading).Where(x => x.race != null && x.race.Humanlike && !x.race.IsMechanoid).ToList();
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("NarutoMod races patch loaded successfully, races affected: ");
            foreach (ThingDef thingDef in list)
            {
                if (thingDef.comps.Find(c => c is NM_CompProperties_RaceComp) == null)
                {
                    thingDef.comps.Add(new NM_CompProperties_RaceComp(300f, 0.025f));
                    stringBuilder.Append(thingDef.defName + "; ");
                }
            }
            Log.Message(stringBuilder.ToString());
        }
    }
}
