// Decompiled with JetBrains decompiler
// Type: NarutoMod.Patches.HumanlikeRacesPatch
// Assembly: NarutoMod, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A24FD7BF-E4B7-40C4-8848-97E48E1CC6B6
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\RimWorld\Mods\SovereignNarutoMod\Assemblies\NarutoMod.dll

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
            stringBuilder.AppendLine("Hediff abilities races patch loaded successfully, races affected: ");
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
