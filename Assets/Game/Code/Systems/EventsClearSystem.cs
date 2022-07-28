using System.Collections.Generic;
using Game.Components;
using Game.Core;
using Leopotam.EcsLite;

namespace Game.Systems
{
	public class EventsClearSystem : IEcsInitSystem, IEcsRunSystem
	{
		private EcsWorld _eventsWorld;
		private int[] _eventEntitiesToRemove;
		
		public void Init(IEcsSystems systems)
		{
			_eventsWorld = systems.GetWorld(GameConstants.EVENTS_WORLD_NAME);
		}

		public void Run(IEcsSystems systems)
		{
			var eventsCount = _eventsWorld.GetAllEntities(ref _eventEntitiesToRemove);
			for (var i = 0; i < eventsCount; ++i)
			{
				_eventsWorld.DelEntity(_eventEntitiesToRemove[i]);				
			}
		}
	}
}