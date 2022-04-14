using NarutoMod.Gizmos;
using NarutoMod.Hediffs;
using NarutoMod.Verbs;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Verse;

namespace NarutoMod.Things
{
    public class NM_Comp_RaceComp : ThingComp
    {
        private float power;
        public List<NM_Verb_AbilityHediff> verbs;

        public Pawn ParentPawn => parent as Pawn;

        public NM_CompProperties_RaceComp Props => props as NM_CompProperties_RaceComp;

        public int Tick => Find.TickManager.TicksGame;

        public float MaxPower => Props.powerBase;

        public float PowerGain => Props.powerGain;

        public float Power => power;

        public List<NM_Verb_AbilityHediff> AllVerbs
        {
            get
            {
                if (verbs == null || verbs.Count == 0)
                {
                    InitVerbsFromZero();
                }

                return verbs;
            }
        }

        public void InitVerbsFromZero()
        {
            verbs = new List<NM_Verb_AbilityHediff>();
            InitVerbs((type, id) =>
           {
               NM_Verb_AbilityHediff instance = (NM_Verb_AbilityHediff)Activator.CreateInstance(type);
               verbs.Add(instance);
               return instance;
           });
        }

        private void InitVerbs(Func<Type, string, NM_Verb_AbilityHediff> creator)
        {
            List<NM_VerbProperties_Ability> propertiesAbilityList = new List<NM_VerbProperties_Ability>();
            foreach (HediffComp hediffComp in ParentPawn.health.hediffSet.GetAllComps().ToList())
            {
                if (hediffComp is NM_HediffComp_Ability hediffCompAbility)
                    propertiesAbilityList.AddRange(hediffCompAbility.Props.verbProps);
            }
            for (int index = 0; index < propertiesAbilityList.Count; ++index)
            {
                NM_VerbProperties_Ability properties = propertiesAbilityList[index];
                string id = "NM_HediffVerbOfMod_" + index.ToString();
                InitVerb(creator(properties.verbClass, id), properties, id);
            }
        }

        private void InitVerb(NM_Verb_AbilityHediff verb, NM_VerbProperties_Ability properties, string id)
        {
            verb.loadID = id;
            verb.verbProps = properties;
            verb.verbTracker = ParentPawn.verbTracker;
            verb.caster = parent;
        }

        public override IEnumerable<Gizmo> CompGetGizmosExtra()
        {
            NM_Comp_RaceComp compRaceComp = this;
            if (compRaceComp.AllVerbs.Count() != 0)
            {
                yield return compRaceComp.CreateGizmoPower();
                if (Prefs.DevMode)
                {
                    Command_Action commandAction1 = new Command_Action();
                    (commandAction1).defaultLabel = "Debug: max power";
                    commandAction1.action = () => { power = MaxPower; };
                    yield return commandAction1;


                    Command_Action commandAction2 = new Command_Action();
                    (commandAction2).defaultLabel = "Debug: reload verbs";
                    commandAction2.action = () => { InitVerbsFromZero(); };
                    yield return commandAction2;
                }
            }
            foreach (NM_Verb_AbilityHediff allVerb in compRaceComp.AllVerbs)
                yield return compRaceComp.CreateVerbTargetCommand(allVerb);
        }

        private NM_Command_HediffAbility CreateVerbTargetCommand(NM_Verb_AbilityHediff verb)
        {
            NM_Command_HediffAbility commandHediffAbility = new NM_Command_HediffAbility
            {
                defaultDesc = verb.Props.description,
                defaultLabel = verb.Props.label,
                verb = verb,
                icon = ContentFinder<Texture2D>.Get(verb.verbProps.commandIcon, true),
                order = 100f
            };
            NM_Command_HediffAbility verbTargetCommand = commandHediffAbility;
            if (!ParentPawn.IsColonistPlayerControlled)
            {
                verbTargetCommand.Disable(null);
            }
            else if (NM_BlackFireUtility.IsBurning(verb.CasterPawn))
                verbTargetCommand.Disable(TranslatorFormattedStringExtensions.Translate("NarutoMod.GUI.CasterIsBurning", verb.CasterPawn.LabelShort));
            else if (verb.CasterPawn.Downed)
                verbTargetCommand.Disable(TranslatorFormattedStringExtensions.Translate("NarutoMod.GUI.CasterIsDowned", verb.CasterPawn.LabelShort));
            else if (!verb.IsReady())
                verbTargetCommand.Disable(TranslatorFormattedStringExtensions.Translate("NarutoMod.GUI.PowerIsLow", verb.Props.powerCost));
            return verbTargetCommand;
        }

        private NM_Gizmo_Power CreateGizmoPower() => new NM_Gizmo_Power()
        {
            comp = this
        };
        public override void CompTick()
        {
            base.CompTick();
            if (Tick % 60 != 0 || AllVerbs == null || AllVerbs.Count == 0)
                return;
            PowerGainTick();
        }

        private void PowerGainTick() => power = Mathf.Clamp(power + PowerGain, 0.0f, MaxPower);

        public void Notify_PowerGain(float gain) => power = Mathf.Clamp(power + gain, 0.0f, MaxPower);

        public override void PostExposeData()
        {
            base.PostExposeData();
            Scribe_Values.Look(ref power, "power", 0.0f, false);
        }
    }
}
