using Game.Components;
using Game.Core;
using Leopotam.EcsLite;

namespace Game.Systems
{
	public class PlayerMoveSystem : IEcsInitSystem, IEcsRunSystem
	{
		private EcsFilter _playerFilter;
		private EcsPool<MoveComponent> _movePool;
		private EcsPool<PlayerMoveRequest> _requestPool;
		private EcsFilter _moveRequestFilter;

		public void Init(IEcsSystems systems)
		{
			var defaultWorld = systems.GetWorld();
			var eventsWorld = systems.GetWorld(GameConstants.EVENTS_WORLD_NAME);
			
			_movePool = defaultWorld.GetPool<MoveComponent>();
			_playerFilter = defaultWorld.Filter<PlayerComponent>().Inc<PositionComponent>().End();
			
			_requestPool = eventsWorld.GetPool<PlayerMoveRequest>();
			_moveRequestFilter = eventsWorld.Filter<PlayerMoveRequest>().End();
		}

		public void Run(IEcsSystems systems)
		{
			if (!_moveRequestFilter.GetSingleEntity(out var moveRequestEntity)
			    || !_playerFilter.GetSingleEntity(out var playerEntity))
			{
				return;
			}

			ref var moveComponent = ref _movePool.Refresh(playerEntity);
			ref var moveRequest = ref _requestPool.Get(moveRequestEntity);
			moveComponent.TargetPosition = moveRequest.TargetPosition;
			moveComponent.Speed = GameConstants.PLAYER_MOVE_SPEED;
		}
	}
}