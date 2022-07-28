using Game.Components;
using Game.Core;
using Game.Services;
using Leopotam.EcsLite;
using UnityEngine;

namespace Game.Systems
{
	public class MoveSystem : IEcsInitSystem, IEcsRunSystem
	{
		private readonly ITimeService _timeService;
		private EcsWorld _world;
		private EcsFilter _filter;
		private EcsPool<MoveComponent> _movePool;
		private EcsPool<PositionComponent> _positionPool;

		public MoveSystem(ITimeService timeService)
		{
			_timeService = timeService;
		}

		public void Init(IEcsSystems systems)
		{
			_world = systems.GetWorld();
			_movePool = _world.GetPool<MoveComponent>();
			_positionPool = _world.GetPool<PositionComponent>();
			_filter = _world.Filter<MoveComponent>().Inc<PositionComponent>().End();
		}

		public void Run(IEcsSystems systems)
		{
			foreach (var entity in _filter)
			{
				ref var move = ref _movePool.Get(entity);
				ref var position = ref _positionPool.Get(entity);
				position.Position = Vector3.MoveTowards(position.Position, move.TargetPosition, _timeService.DeltaTime * move.Speed);
				
				if (Vector3.SqrMagnitude(position.Position - move.TargetPosition) <=
				    GameConstants.SQR_DISTANCE_MIN_THRESHOLD)
				{
					_movePool.Del(entity);
				}
			}
		}
	}
}