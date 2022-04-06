using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Verse.AI;

namespace SovereignNarutoMod.AI.JobDrivers
{
	public class JobDriver_SacrificeSelf : JobDriver
	{
		public override bool TryMakePreToilReservations(bool errorOnFailed)
		{
			return ReservationUtility.Reserve(this.pawn, this.job.targetA, this.job, 1, -1, null, errorOnFailed);
		}

		[IteratorStateMachine(typeof(JobDriver_SacrificeSelf.< MakeNewToils > d__1))]
		protected override IEnumerable<Toil> MakeNewToils()
		{
			JobDriver_SacrificeSelf.< MakeNewToils > d__1 expr_07 = new JobDriver_SacrificeSelf.< MakeNewToils > d__1(-2);
			expr_07.<> 4__this = this;
			return expr_07;
		}
	}
}
