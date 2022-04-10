// Decompiled with JetBrains decompiler
// Type: NarutoMod.Gizmos.Gizmo_Power
// Assembly: NarutoMod, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A24FD7BF-E4B7-40C4-8848-97E48E1CC6B6
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\RimWorld\Mods\SovereignNarutoMod\Assemblies\NarutoMod.dll

using NarutoMod.Things;
using System;
using UnityEngine;
using Verse;

namespace NarutoMod.Gizmos
{
    [StaticConstructorOnStartup]
    public class NM_Gizmo_Power : Gizmo
    {
        private static readonly Texture2D FullShieldBarTex = SolidColorMaterials.NewSolidColorTexture(new Color(0.2f, 0.2f, 0.24f));
        private static readonly Texture2D EmptyShieldBarTex = SolidColorMaterials.NewSolidColorTexture(Color.clear);
        public NM_Comp_RaceComp comp;

        public NM_Gizmo_Power() => order = -1000f;

        public override float GetWidth(float maxWidth) => 140f;

        public override GizmoResult GizmoOnGUI(
          Vector2 topLeft,
          float maxWidth,
          GizmoRenderParms parms)
        {
            Rect rect1 = new Rect(topLeft.x, topLeft.y, GetWidth(maxWidth), 75f);
            Rect rect2 = GenUI.ContractedBy(rect1, 6f);
            Widgets.DrawWindowBackground(rect1);
            Rect rect3 = rect2;
            rect3.height = rect1.height / 2f;
            Text.Font = 0;
            Text.Anchor = (TextAnchor)4;
            Widgets.Label(rect3, Translator.Translate("NarutoMod.GUI.Power"));
            Rect rect4 = rect2;
            rect4.yMin = rect2.y + rect2.height / 2f;
            float num1 = comp.Power / comp.MaxPower;
            Widgets.FillableBar(rect4, num1, FullShieldBarTex, EmptyShieldBarTex, false);
            Text.Font = (GameFont)1;
            Rect rect5 = rect4;
            float num2 = comp.Power;
            string str1 = num2.ToString("F0");
            num2 = comp.MaxPower;
            string str2 = num2.ToString("F0");
            string str3 = str1 + " / " + str2;
            Widgets.Label(rect5, str3);
            Text.Anchor = 0;
            if (Mouse.IsOver(rect4))
            {
                string str4 = TranslatorFormattedStringExtensions.Translate("NarutoMod.GUI.PowerGain", Math.Round(comp.PowerGain * 1000.0, 2));
                TooltipHandler.TipRegion(rect4, str4);
            }
            return new GizmoResult(0);
        }
    }
}
