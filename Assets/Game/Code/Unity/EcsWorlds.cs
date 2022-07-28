using Leopotam.EcsLite;

namespace Game.Unity
{
	public class EcsWorlds
	{
		public readonly EcsWorld DefaultWorld;
		public readonly EcsWorld EventsWorld;

		public EcsWorlds(EcsWorld defaultWorld, EcsWorld eventsWorld)
		{
			DefaultWorld = defaultWorld;
			EventsWorld = eventsWorld;
		}
	}
}