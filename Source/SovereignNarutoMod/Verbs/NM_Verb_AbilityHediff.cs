using NarutoMod.Things;
using RimWorld;
using System.Collections.Generic;
using Verse;
using Verse.AI;

namespace NarutoMod.Verbs
{
    public abstract class NM_Verb_AbilityHediff : Verb_CastBase
    {
        public List<Pair<Effecter, TargetInfo>> maintainedEffecters = new List<Pair<Effecter, TargetInfo>>();

        private NM_Comp_RaceComp Comp => CasterPawn.GetComp<NM_Comp_RaceComp>();

        public NM_VerbProperties_Ability Props => verbProps as NM_VerbProperties_Ability;

        public int Tick => Find.TickManager.TicksGame;

        //  #######################
        //  # Cooldown Code START #
        //  #######################

        private int cooldownTicks;
        private int cooldownTicksDuration;
        public NM_VerbProperties_Ability prop;
        public List<NM_Comp_RaceComp> verbs;
        public Pawn pawn;

        public int CooldownTicksRemaining
        {
            get
            {
                return this.cooldownTicks;
            }
        }

        public int CooldownTicksTotal
        {
            get
            {
                return this.cooldownTicksDuration;
            }
        }

        public VerbTracker VerbTracker
        {
            get
            {
                if (this.verbTracker == null)
                {
                    this.verbTracker = new VerbTracker((IVerbOwner)this);
                }
                return this.verbTracker;
            }
        }

        public bool HasCooldown
        {
            get
            {
                return this.prop.cooldownTicksRange != default(IntRange);
            }
        }

        public virtual bool CanCast
        {
            get
            {
                return this.cooldownTicks <= 0;
            }
        }


        public virtual bool GizmoDisabled(out string reason)
        {
            if (!this.CanCast)
            {
                reason = "AbilityOnCooldown".Translate(this.cooldownTicks.ToStringTicksToPeriod(true, false, true, true));
                return true;
            }
            if (!this.verbs.NullOrEmpty<NM_Comp_RaceComp>())
            {
                for (int i = 0; i < this.verbs.Count; i++)
                {
                    if (this.verbs[i].GizmoDisabled(out reason))
                    {
                        return true;
                    }
                }
            }
            reason = null;
            return false;

        }

        public void StartCooldown(int ticks)
        {
            this.cooldownTicksDuration = ticks;
            this.cooldownTicks = this.cooldownTicksDuration;
        }

        public virtual void AbilityTick()
        {
            this.VerbTracker.VerbsTick();
            if (this.cooldownTicks > 0)
            {
                this.cooldownTicks--;
                if (this.cooldownTicks == 0 && this.prop.sendLetterOnCooldownComplete)
                {
                    Find.LetterStack.ReceiveLetter("AbilityReadyLabel".Translate(this.prop.LabelCap), "AbilityReadyText".Translate(this.pawn, this.prop.label), LetterDefOf.NeutralEvent, new LookTargets(this.pawn), null, null, null, null);
                }
            }
        }

        //  #####################
        //  # Cooldown Code END #
        //  #####################


        public virtual bool IsReady() => (double)Comp.Power >= Props.powerCost && Comp.Power != 0;

        public override bool Available() => IsReady() && base.Available();      

        public override void ExposeData()
        {
        }

        protected override bool TryCastShot() => true;

        public override void WarmupComplete()
        {
            base.WarmupComplete();
            Comp.Notify_PowerGain(-Props.powerCost);
            StartCooldown(Props.cooldownTicks);
        }

        public void AddEffecterToMaintain(Effecter eff, IntVec3 pos, int ticks, Pawn pawn, Map map = null)
        {
            eff.ticksLeft = ticks;
            maintainedEffecters.Add(new Pair<Effecter, TargetInfo>(eff, new TargetInfo(pos, map ?? pawn.Map, false)));
        }
    }
}
