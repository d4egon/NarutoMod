﻿using System;
using System.Collections.Generic;
using Verse;

namespace SovereignNarutoMod.Verbs
{
	public class VerbProperties_Ability : VerbProperties
	{
		public float powerCost;

		[MustTranslate]
		public string description;

		public HediffDef hediffDef;

		public FleckDef fleckDef;

		public ThingDef moteDef;

		public float damageAmount;

		public float severity;

		public FloatRange conditionDaysRange;

		public GameConditionDef gameConditionDef;

		public List<IntVec2> pattern;

		public bool ignoreRange;
	}
}
