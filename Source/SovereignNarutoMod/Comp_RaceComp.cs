using SovereignNarutoMod.Gizmos;
using SovereignNarutoMod.Hediffs;
using SovereignNarutoMod.Verbs;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using Verse;

namespace SovereignNarutoMod.Things
{
	public class Comp_RaceComp : ThingComp
	{
		private float power;

		public List<Verb_AbilityHediff> verbs;

		public Pawn parentPawn
		{
			get
			{
				return this.parent as Pawn;
			}
		}

		public CompProperties_RaceComp Props
		{
			get
			{
				return this.props as CompProperties_RaceComp;
			}
		}

		public int Tick
		{
			get
			{
				return Find.get_TickManager().get_TicksGame();
			}
		}

		public float MaxPower
		{
			get
			{
				return this.Props.powerBase;
			}
		}

		public float PowerGain
		{
			get
			{
				return this.Props.powerGain;
			}
		}

		public float Power
		{
			get
			{
				return this.power;
			}
		}

		public List<Verb_AbilityHediff> AllVerbs
		{
			get
			{
				if (this.verbs == null || this.verbs.Count == 0)
				{
					this.InitVerbsFromZero();
				}
				return this.verbs;
			}
		}

		public void InitVerbsFromZero()
		{
			this.verbs = new List<Verb_AbilityHediff>();
			this.InitVerbs(delegate (Type type, string id)
			{
				Verb_AbilityHediff verb_AbilityHediff = (Verb_AbilityHediff)Activator.CreateInstance(type);
				this.verbs.Add(verb_AbilityHediff);
				return verb_AbilityHediff;
			});
		}

		private void InitVerbs(Func<Type, string, Verb_AbilityHediff> creator)
		{
			List<VerbProperties_Ability> list = new List<VerbProperties_Ability>();
			using (List<HediffComp>.Enumerator enumerator = this.parentPawn.health.hediffSet.GetAllComps().ToList<HediffComp>().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					HediffComp_Ability hediffComp_Ability = enumerator.Current as HediffComp_Ability;
					if (hediffComp_Ability != null)
					{
						list.AddRange(hediffComp_Ability.Props.verbProps);
					}
				}
			}
			for (int i = 0; i < list.Count; i++)
			{
				VerbProperties_Ability verbProperties_Ability = list[i];
				string text = "HediffVerbOfMod_" + i.ToString();
				this.InitVerb(creator(verbProperties_Ability.verbClass, text), verbProperties_Ability, text);
			}
		}

		private void InitVerb(Verb_AbilityHediff verb, VerbProperties_Ability properties, string id)
		{
			verb.loadID = id;
			verb.verbProps = properties;
			verb.verbTracker = this.parentPawn.verbTracker;
			verb.caster = this.parent;
		}

		[IteratorStateMachine(typeof(Comp_RaceComp.< CompGetGizmosExtra > d__19))]
		public override IEnumerable<Gizmo> CompGetGizmosExtra()
		{
			Comp_RaceComp.< CompGetGizmosExtra > d__19 expr_07 = new Comp_RaceComp.< CompGetGizmosExtra > d__19(-2);
			expr_07.<> 4__this = this;
			return expr_07;
		}

		private Command_HediffAbility CreateVerbTargetCommand(Verb_AbilityHediff verb)
		{
			Command_HediffAbility command_HediffAbility = new Command_HediffAbility
			{
				defaultDesc = verb.Props.description,
				defaultLabel = verb.Props.label,
				verb = verb,
				icon = ContentFinder<Texture2D>.Get(verb.verbProps.commandIcon, true),
				order = 100f
			};
			if (!this.parentPawn.get_IsColonistPlayerControlled())
			{
				command_HediffAbility.Disable(null);
			}
			else if (AmaterasuFireUtility.IsBurning(verb.get_CasterPawn()))
			{
				command_HediffAbility.Disable(TranslatorFormattedStringExtensions.Translate("NarutoMod.GUI.CasterIsBurning", verb.get_CasterPawn().get_LabelShort()));
			}
			else if (verb.get_CasterPawn().get_Downed())
			{
				command_HediffAbility.Disable(TranslatorFormattedStringExtensions.Translate("NarutoMod.GUI.CasterIsDowned", verb.get_CasterPawn().get_LabelShort()));
			}
			else if (!verb.IsReady())
			{
				command_HediffAbility.Disable(TranslatorFormattedStringExtensions.Translate("NarutoMod.GUI.PowerIsLow", verb.Props.powerCost));
			}
			return command_HediffAbility;
		}

		private Gizmo_Power CreateGizmoPower()
		{
			return new Gizmo_Power
			{
				comp = this
			};
		}

		public override void CompTick()
		{
			base.CompTick();
			if (this.Tick % 60 != 0)
			{
				return;
			}
			if (this.AllVerbs == null || this.AllVerbs.Count == 0)
			{
				return;
			}
			this.PowerGainTick();
		}

		private void PowerGainTick()
		{
			this.power = Mathf.Clamp(this.power + this.PowerGain, 0f, this.MaxPower);
		}

		public void Notify_PowerGain(float gain)
		{
			this.power = Mathf.Clamp(this.power + gain, 0f, this.MaxPower);
		}

		public override void PostExposeData()
		{
			base.PostExposeData();
			Scribe_Values.Look<float>(ref this.power, "power", 0f, false);
		}
	}
}
