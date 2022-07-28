using System.Diagnostics.CodeAnalysis;
using Game.Components;
using Game.Core;
using Leopotam.EcsLite;

namespace Game.Systems
{
	[SuppressMessage("ReSharper", "NotAccessedVariable")]
	public class DoorsSpawnSystem : IEcsInitSystem
	{
		private readonly LevelData _levelData;
		private readonly EcsMessenger _messenger;

		public DoorsSpawnSystem(LevelData levelData, EcsMessenger ecsMessenger)
		{
			_levelData = levelData;
			_messenger = ecsMessenger;
		}

		public void Init(IEcsSystems systems)
		{
			var world = systems.GetWorld();
			var doorsPool = world.GetPool<DoorComponent>();
			
			foreach (var doorData in _levelData.Doors)
			{
				var doorEntity = world.NewEntity();
				ref var door = ref doorsPool.Add(doorEntity);
				door.ActivatorPosition = doorData.ActivatorPosition;
				
				_messenger.RaiseEvent(new DoorEntityCreatedEvent(){Entity = doorEntity, DoorConfigId = doorData.ConfigId});
			}
		}
	}
}