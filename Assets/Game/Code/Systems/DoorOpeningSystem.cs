using Game.Components;
using Game.Core;
using Leopotam.EcsLite;
using UnityEngine;
// ReSharper disable NotAccessedVariable

namespace Game.Systems
{
	public class DoorOpeningSystem : IEcsInitSystem, IEcsRunSystem
	{
		private EcsWorld _world;
		private EcsFilter _buttonEntities;
		private EcsFilter _playerEntities;
		private EcsPool<PositionComponent> _positionPool;
		private EcsPool<DoorComponent> _doorsPool;
		private EcsPool<TimeComponent> _timePool;
		private EcsFilter _timeFilter;

		private float _sqrButtonRadius;

		public void Init(IEcsSystems systems)
		{
			_world = systems.GetWorld();
			
			_buttonEntities = _world.Filter<DoorComponent>().End();
			_playerEntities = _world.Filter<PlayerComponent>().Inc<PositionComponent>().End();
			_timeFilter = _world.Filter<TimeComponent>().End();
			_timePool = _world.GetPool<TimeComponent>();

			_positionPool = _world.GetPool<PositionComponent>();
			_doorsPool = _world.GetPool<DoorComponent>();

			_sqrButtonRadius = GameConstants.DOOR_ACTIVATOR_RADIUS * GameConstants.DOOR_ACTIVATOR_RADIUS; 
		}

		public void Run(IEcsSystems systems)
		{
			foreach (var playerEntity in _playerEntities)
			{
				var playerPosition = _positionPool.Get(playerEntity).Position;
				_timeFilter.GetSingleEntity(out var timeEntity);
				foreach (var doorEntity in _buttonEntities)
				{
					ref var door = ref _doorsPool.Get(doorEntity);
					door.Activated = Vector3.SqrMagnitude(door.ActivatorPosition - playerPosition) <= _sqrButtonRadius;
					if (!door.Activated)
					{
						continue;
					}
					
					ref var timeComponent = ref _timePool.Get(timeEntity);
					door.OpeningProgress = Mathf.Min(
						door.OpeningProgress + timeComponent.DeltaTime * GameConstants.DOOR_OPENING_PROGRESS_SPEED,
						1f);
				}	
			}
		}
	}
}