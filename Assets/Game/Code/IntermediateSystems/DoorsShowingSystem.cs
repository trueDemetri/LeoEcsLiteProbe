using Game.Components;
using Game.Core;
using Game.Unity;
using Leopotam.EcsLite;

namespace Game.Systems.IntermediateSystems
{
	public class DoorsShowingSystem : IEcsInitSystem, IEcsRunSystem
	{
		private readonly SceneDoorsService _sceneDoorsService;
		private EcsFilter _doorEntityCreatedEvents;
		private EcsPool<DoorEntityCreatedEvent> _eventsPool;

		public DoorsShowingSystem(SceneDoorsService sceneDoorsService)
		{
			_sceneDoorsService = sceneDoorsService;
		}

		public void Init(IEcsSystems systems)
		{
			var world = systems.GetWorld(GameConstants.EVENTS_WORLD_NAME);
			_doorEntityCreatedEvents = world.Filter<DoorEntityCreatedEvent>().End();
			_eventsPool = world.GetPool<DoorEntityCreatedEvent>();
		}

		public void Run(IEcsSystems systems)
		{
			foreach (var doorCreatedEntity in _doorEntityCreatedEvents)
			{
				ref var eventComponent = ref _eventsPool.Get(doorCreatedEntity);
				_sceneDoorsService.AssignDoorViewToEntity(eventComponent.DoorConfigId, eventComponent.Entity);
			}
		}
	}
}