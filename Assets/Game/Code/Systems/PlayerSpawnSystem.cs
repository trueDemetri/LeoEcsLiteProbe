using System.Diagnostics.CodeAnalysis;
using Game.Components;
using Game.Core;
using Leopotam.EcsLite;
using UnityEngine;

namespace Game.Systems
{
	[SuppressMessage("ReSharper", "NotAccessedVariable")]
	public class PlayerSpawnSystem : IEcsInitSystem
	{
		private readonly Vector3 _playerSpawnPosition;
		
		public PlayerSpawnSystem(LevelData levelData)
		{
			_playerSpawnPosition = levelData.PlayerSpawnPosition;
		}

		public void Init(IEcsSystems systems)
		{
			var defaultWorld = systems.GetWorld();
			var playerPool = defaultWorld.GetPool<PlayerComponent>();
			var positionPool = defaultWorld.GetPool<PositionComponent>();

			var playerEntity = defaultWorld.NewEntity();
			playerPool.Add(playerEntity);
			ref var playerPositionComponent = ref positionPool.Add(playerEntity);
			playerPositionComponent.Position = _playerSpawnPosition;
		}
	}
}