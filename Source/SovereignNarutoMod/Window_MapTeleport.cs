using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Verse;

namespace SovereignNarutoMod.GUI
{
	public class Window_MapTeleport : Window
	{
		private Vector2 scrollPosition = Vector2.zero;

		private Pawn pawn;

		public override Vector2 InitialSize
		{
			get
			{
				return new Vector2(600f, 400f);
			}
		}

		private List<Map> maps
		{
			get
			{
				return (from x in Find.get_Maps()
						where x != this.pawn.get_Map()
						select x).ToList<Map>();
			}
		}

		public Window_MapTeleport(Pawn pawn)
		{
			this.draggable = false;
			this.resizeable = false;
			this.forcePause = true;
			this.doCloseX = true;
			this.pawn = pawn;
		}

		public override void DoWindowContents(Rect inRect)
		{
			Rect rect = new Rect(10f, 10f, this.get_InitialSize().x - 60f, 30f);
			Text.set_Anchor(TextAnchor.MiddleCenter);
			Text.set_Font(2);
			Widgets.Label(rect, Translator.Translate("SovereignNarutoMod.GUI.MapsList"));
			Text.set_Font(1);
			if (GenList.NullOrEmpty<Map>(this.maps))
			{
				Widgets.Label(new Rect(rect.x, rect.y + rect.height, rect.width, 60f), Translator.Translate("SovereignNarutoMod.GUI.NullMapsList"));
			}
			Text.set_Anchor(TextAnchor.UpperLeft);
			Rect rect2 = new Rect(30f, 70f, this.get_InitialSize().x - 70f, 240f);
			Rect rect3 = new Rect(0f, 0f, rect2.x, (float)(this.maps.Count * 30));
			Widgets.BeginScrollView(rect2, ref this.scrollPosition, rect3, true);
			int num = 0;
			float width = rect2.width - 20f;
			foreach (Map current in this.maps)
			{
				if (Widgets.ButtonText(new Rect(0f, (float)(num * 30), width, 30f), current.get_TileInfo().biome.get_LabelCap() + string.Format(", {0}: ", Translator.Translate("NarutoMod.GUI.TileId")) + current.get_Tile().ToString(), true, true, true))
				{
					IntVec3 intVec;
					if (this.TryFindShipChunkDropCell(current.get_Center(), current, current.get_Size().x / 2, out intVec))
					{
						this.pawn.DeSpawn(0);
						GenSpawn.Spawn(this.pawn, intVec, current, 0);
						this.pawn.Notify_Teleported(true, true);
						this.Close(true);
					}
					else
					{
						Messages.Message(Translator.Translate("SovereignNarutoMod.Messages.TryFindCellFalse"), MessageTypeDefOf.NegativeEvent, true);
					}
				}
				num++;
			}
			Widgets.EndScrollView();
		}

		private bool TryFindShipChunkDropCell(IntVec3 nearLoc, Map map, int maxDist, out IntVec3 pos)
		{
			return CellFinderLoose.TryFindSkyfallerCell(ThingDefOf.ShipChunkIncoming, map, ref pos, 10, nearLoc, maxDist, true, false, false, false, true, false, null);
		}
	}
}
