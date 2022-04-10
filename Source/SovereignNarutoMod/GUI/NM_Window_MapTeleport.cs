// Decompiled with JetBrains decompiler
// Type: NarutoMod.GUI.Window_MapTeleport
// Assembly: NarutoMod, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A24FD7BF-E4B7-40C4-8848-97E48E1CC6B6
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\RimWorld\Mods\SovereignNarutoMod\Assemblies\NarutoMod.dll

using RimWorld;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Verse;

namespace NarutoMod.GUI
{
    public class NM_Window_MapTeleport : Window
    {
        private Vector2 scrollPosition = Vector2.zero;
        private Pawn pawn;

        public override Vector2 InitialSize => new Vector2(600f, 400f);

        private List<Map> Maps => ((IEnumerable<Map>)Find.Maps).Where(x => x != pawn.Map).ToList();

        public NM_Window_MapTeleport(Pawn pawn)
        {
            draggable = false;
            resizeable = false;
            forcePause = true;
            doCloseX = true;
            this.pawn = pawn;
        }

        public override void DoWindowContents(Rect inRect)
        {
            Rect rect1;
            // ISSUE: explicit constructor call
            rect1 = new Rect(10f, 10f, base.InitialSize.x - 60f, 30f);
            Text.Anchor = (TextAnchor)4;
            Text.Font = (GameFont)2;
            Widgets.Label(rect1, Translator.Translate("NarutoMod.GUI.MapsList"));
            Text.Font = (GameFont)1;
            if (GenList.NullOrEmpty(Maps))
                Widgets.Label(new Rect(rect1.x, rect1.y + rect1.height, rect1.width, 60f), Translator.Translate("NarutoMod.GUI.NullMapsList"));
            Text.Anchor = 0;
            Rect rect2;
            // ISSUE: explicit constructor call
            rect2 = new Rect(30f, 70f, base.InitialSize.x - 70f, 240f);
            Rect rect3;
            // ISSUE: explicit constructor call
            rect3 = new Rect(0.0f, 0.0f, rect2.x, Maps.Count * 30);
            Widgets.BeginScrollView(rect2, ref scrollPosition, rect3, true);
            int num1 = 0;
            float num2 = rect2.width - 20f;
            foreach (Map map in Maps)
            {
                if (Widgets.ButtonText(new Rect(0.0f, num1 * 30, num2, 30f), map.TileInfo.biome.LabelCap + map.Tile.ToString(), true, true, true))
                    {
                    if (TryFindShipChunkDropCell(map.Center, map, map.Size.x / 2, out IntVec3 pos))
                    {
                        pawn.DeSpawn(0);
                        GenSpawn.Spawn(pawn, pos, map, 0);
                        pawn.Notify_Teleported(true, true);
                        Close(true);
                    }
                    else
                        Messages.Message(Translator.Translate("NarutoMod.Messages.TryFindCellFalse"), MessageTypeDefOf.NegativeEvent, true);
                }
                ++num1;
            }
            Widgets.EndScrollView();
        }

        private bool TryFindShipChunkDropCell(IntVec3 nearLoc, Map map, int maxDist, out IntVec3 pos) => CellFinderLoose.TryFindSkyfallerCell(ThingDefOf.ShipChunkIncoming, map, out pos, 10, nearLoc, maxDist, true, false, false, false, true, false, null);
    }
}