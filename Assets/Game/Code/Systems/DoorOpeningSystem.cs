using Game.Components;
using Game.Core;
using Game.Services;
using Leopotam.EcsLite;
using UnityEngine;
// ReSharper disable NotAccessedVariable

namespace Game.Systems
{
	public class DoorOpeningSystem : IEcsInitSystem, IEcsRunSystem
	{
		private readonly ITimeService _timeService;
		
		private EcsWorld _world;
		private EcsFilter _buttonEntities;
		private EcsFilter _playerEntities;
		private EcsPool<PositionComponent> _positionPool;
		private EcsPool<DoorComponent> _doorsPool;

		private float _sqrButtonRadius;

		public DoorOpeningSystem(ITimeService timeService)
		{
			_timeService = timeService;
		}

		public void Init(IEcsSystems systems)
		{
			_world = systems.GetWorld();
			
			_buttonEntities = _world.Filter<DoorComponent>().End();
			_playerEntities = _world.Filter<PlayerComponent>().Inc<PositionComponent>().End();

			_positionPool = _world.GetPool<PositionComponent>();
			_doorsPool = _world.GetPool<DoorComponent>();

			_sqrButtonRadius = GameConstants.DOOR_ACTIVATOR_RADIUS * GameConstants.DOOR_ACTIVATOR_RADIUS; 
		}

		public void Run(IEcsSystems systems)
		{
			foreach (var playerEntity in _playerEntities)
			{
				var playerPosition = _positionPool.Get(playerEntity).Position;
				foreach (var doorEntity in _buttonEntities)
				{
					ref var door = ref _doorsPool.Get(doorEntity);
					door.Activated = Vector3.SqrMagnitude(door.ActivatorPosition - playerPosition) <= _sqrButtonRadius;
					if (!door.Activated)
					{
						continue;
					}
					door.OpeningProgress = Mathf.Min(
						door.OpeningProgress + _timeService.DeltaTime * GameConstants.DOOR_OPENING_PROGRESS_SPEED,
						1f);	
				}	
			}
		}
	}
}