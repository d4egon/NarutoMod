using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using Verse;

namespace SovereignNarutoMod.Verbs
{
	public class Verb_TeleportTargetAway : Verb_AbilityHediff
	{
		public override void WarmupComplete()
		{
			base.WarmupComplete();
			base.AddEffecterToMaintain(EffecterDefOf.Skip_Entry.Spawn(this.currentTarget.get_Pawn().get_Position(), this.get_CasterPawn().get_Map(), 1f), this.currentTarget.get_Cell(), 120, this.currentTarget.get_Pawn(), null);
			base.AddEffecterToMaintain(EffecterDefOf.Skip_Entry.Spawn(this.get_CasterPawn().get_Position(), this.get_CasterPawn().get_Map(), 1f), this.get_CasterPawn().get_Position(), 120, this.get_CasterPawn(), null);
			List<IntVec3> list = (from x in GenRadial.RadialCellsAround(this.get_CasterPawn().get_Position(), base.Props.range, false)
								  where IntVec3Utility.DistanceTo(x, this.get_CasterPawn().get_Position()) > base.Props.range - 5f && GenGrid.Walkable(x, this.caster.get_Map())
								  select x).ToList<IntVec3>();
			if (!GenList.NullOrEmpty<IntVec3>(list))
			{
				IntVec3 position = GenCollection.RandomElement<IntVec3>(list);
				this.currentTarget.get_Pawn().set_Position(position);
				this.currentTarget.get_Pawn().stances.stunner.StunFor(120, this.get_CasterPawn(), false, false);
				this.currentTarget.get_Pawn().Notify_Teleported(true, true);
				return;
			}
			Messages.Message(Translator.Translate("NarutoMod.Messages.CellsNullOrEmpty"), MessageTypeDefOf.NeutralEvent, true);
		}
	}
}
