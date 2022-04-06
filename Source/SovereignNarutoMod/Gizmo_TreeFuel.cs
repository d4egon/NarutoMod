using SovereignNarutoMod.Things;
using System;
using UnityEngine;
using Verse;

namespace SovereignNarutoMod.Gizmos
{
	[StaticConstructorOnStartup]
	public class Gizmo_TreeFuel : Gizmo
	{
		private static readonly Texture2D FullShieldBarTex = SolidColorMaterials.NewSolidColorTexture(new Color(0.2f, 0.2f, 0.24f));

		private static readonly Texture2D EmptyShieldBarTex = SolidColorMaterials.NewSolidColorTexture(Color.clear);

		public Building_FruitTree tree;

		public Gizmo_TreeFuel()
		{
			this.order = -90f;
		}

		public override float GetWidth(float maxWidth)
		{
			return 140f;
		}

		public override GizmoResult GizmoOnGUI(Vector2 topLeft, float maxWidth, GizmoRenderParms parms)
		{
			Rect rect = new Rect(topLeft.x, topLeft.y, this.GetWidth(maxWidth), 75f);
			Rect rect2 = GenUI.ContractedBy(rect, 6f);
			Widgets.DrawWindowBackground(rect);
			Rect rect3 = rect2;
			rect3.height = rect.height / 2f;
			Text.set_Font(0);
			Text.set_Anchor(TextAnchor.MiddleCenter);
			Widgets.Label(rect3, Translator.Translate("NarutoMod.GUI.TreeFuel"));
			Rect rect4 = rect2;
			rect4.yMin = rect2.y + rect2.height / 2f;
			float num = this.tree.Fuel / this.tree.FuelMax;
			Widgets.FillableBar(rect4, num, Gizmo_TreeFuel.FullShieldBarTex, Gizmo_TreeFuel.EmptyShieldBarTex, false);
			Text.set_Font(1);
			Widgets.Label(rect4, this.tree.Fuel.ToString("F0") + " / " + this.tree.FuelMax.ToString("F0"));
			Text.set_Anchor(TextAnchor.UpperLeft);
			if (Mouse.IsOver(rect4))
			{
				string text = TranslatorFormattedStringExtensions.Translate("NarutoMod.GUI.TreeFuelPerDay", Math.Round((double)this.tree.FuelPerDay, 2));
				TooltipHandler.TipRegion(rect4, text);
			}
			return new GizmoResult(0);
		}
	}
}
