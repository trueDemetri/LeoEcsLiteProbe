using Game.Components;
using Game.Core;
using Leopotam.EcsLite;
using UnityEngine;

namespace Game.Systems.IntermediateSystems
{
	public class PlayerPointAndClickSystem : IEcsRunSystem
	{
		private readonly Camera _mainCamera;
		private readonly EcsMessenger _ecsMessenger;
		private EcsFilter _playerFilter;
		private EcsPool<MoveComponent> _movePool;

		public PlayerPointAndClickSystem(Camera mainCamera, EcsMessenger ecsMessenger)
		{
			_mainCamera = mainCamera;
			_ecsMessenger = ecsMessenger;
		}

		public void Run(IEcsSystems systems)
		{
			if (!Input.GetMouseButton(0))
			{
				return;
			}

			var ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
			if (!Physics.Raycast(ray, out var groundHit, 1000f, GameConstants.GROUND_HIT_MASK))
			{
				return;
			}
			
			_ecsMessenger.RaiseEvent(new PlayerMoveRequest(groundHit.point));
		}
	}
}