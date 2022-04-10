using Verse;

namespace NarutoMod
{
    public class NM_BlackSpark : Projectile
	{
		protected override void Impact(Thing hitThing)
		{
			Map map = base.Map;
			base.Impact(hitThing);
			NM_BlackFireUtility.TryStartFireIn(base.Position, map, 0.1f);
		}
	}
}
