using Game.Components;
using Leopotam.EcsLite;
using UnityEngine;
using Zenject;
// ReSharper disable PossibleInvalidOperationException

namespace Game.Unity
{
	public class PlayerView : MonoBehaviour
	{
		[SerializeField] private Animator _animator;

		private EcsPool<PositionComponent> _positionPool;
		private EcsPool<MoveComponent> _movePool;
		private EcsFilter _playerEntityFilter;
		private int _isMovingHash;
		private Transform _ownTransform;

		[Inject]
		public void Construct(EcsWorlds ecsWorlds)
		{
			var world = ecsWorlds.DefaultWorld;
			_positionPool = world.GetPool<PositionComponent>();
			_movePool = world.GetPool<MoveComponent>();
			_playerEntityFilter = world.Filter<PlayerComponent>().Inc<PositionComponent>().End();
			
			_isMovingHash = Animator.StringToHash("isMoving");
			_ownTransform = transform;
		}
		
		private void Update()
		{
			foreach (var playerEntity in _playerEntityFilter)
			{
				SyncPosition(playerEntity);
				SyncMovingAndDirection(playerEntity);				
			}
		}

		private void SyncMovingAndDirection(int playerEntity)
		{
			if (!_movePool.Has(playerEntity))
			{
				_animator.SetBool(_isMovingHash, false);
				return;
			}

			_animator.SetBool(_isMovingHash, true);
			ref var moveComponent = ref _movePool.Get(playerEntity);
			_ownTransform.forward = (moveComponent.TargetPosition - _ownTransform.position).normalized;
		}

		private void SyncPosition(int playerEntity)
		{
			ref var positionComponent = ref _positionPool.Get(playerEntity);
			_ownTransform.position = positionComponent.Position;
		}
	}
}