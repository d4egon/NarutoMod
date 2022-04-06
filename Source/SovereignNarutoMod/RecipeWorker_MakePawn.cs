using SovereignNarutoMod.ModExtensions;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using Verse;

namespace SovereignNarutoMod.Recipes
{
	public class RecipeWorker_MakePawn : RecipeWorker
	{
		public override void Notify_IterationCompleted(Pawn billDoer, List<Thing> ingredients)
		{
			base.Notify_IterationCompleted(billDoer, ingredients);
			this.Spawn(billDoer.get_CurJob().targetA.get_Thing());
		}

		private void Spawn(Thing parent)
		{
			IntVec3 position = parent.get_Position();
			PawnKindDef pawnForSpawn = this.recipe.GetModExtension<DefModExtension_RecipeMakePawn>().pawnForSpawn;
			Pawn arg_AF_0 = PawnGenerator.GeneratePawn(new PawnGenerationRequest(pawnForSpawn, parent.get_Faction(), 2, -1, false, false, false, false, true, false, 1f, false, true, true, true, false, false, false, false, 0f, 0f, null, 1f, null, null, null, null, null, new float?(pawnForSpawn.race.race.lifeStageAges.First<LifeStageAge>().minAge), null, null, null, null, null, null, null, false, false, false));
			IntVec3 intVec = CellFinder.RandomClosewalkCellNear(position, parent.get_Map(), 1, null);
			GenSpawn.Spawn(arg_AF_0, intVec, parent.get_Map(), 0);
			Pawn pawn = parent.get_Map().mapPawns.get_FreeColonists().FirstOrDefault<Pawn>();
			Pawn pawn2 = arg_AF_0 as Pawn;
			if (pawn2 != null && pawn != null)
			{
				string text;
				string text2;
				InteractionWorker_RecruitAttempt.DoRecruit(pawn2.get_Map().mapPawns.get_FreeColonists().FirstOrDefault<Pawn>(), pawn2, ref text, ref text2, false, false);
			}
		}
	}
}
