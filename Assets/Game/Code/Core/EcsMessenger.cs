using Game.Components;
using Leopotam.EcsLite;

namespace Game.Core
{
	public class EcsMessenger
	{
		private readonly EcsWorld _eventsWorld;

		public EcsMessenger(EcsWorld eventsWorld)
		{
			_eventsWorld = eventsWorld;
		}

		public void RaiseEvent<TEvent>(TEvent eventData) where TEvent : struct
		{
			var eventDataPool = _eventsWorld.GetPool<TEvent>();
			var eventEntity = _eventsWorld.NewEntity();
			ref var eventDataComponent = ref eventDataPool.Add(eventEntity);
			eventDataComponent = eventData;
		}
	}
}