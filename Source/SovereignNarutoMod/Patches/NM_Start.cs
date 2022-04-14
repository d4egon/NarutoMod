using HarmonyLib;
using Verse;

namespace NarutoMod.Patches
{
    [StaticConstructorOnStartup]
    public static class NM_Start
    {
        static NM_Start() => new Harmony("Sovereign.NarutoMod").PatchAll();
    }
}
