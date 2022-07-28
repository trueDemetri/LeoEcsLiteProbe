using System;
using System.Collections.Generic;
using Game.Core;
using Leopotam.EcsLite;
using Zenject;

namespace Game.Unity
{
	public class EcsMotor : IInitializable, ITickable, IDisposable
	{
		private readonly IEcsSystems _ecsSystems;

		public EcsMotor(EcsWorlds ecsWorlds, List<IEcsSystem> systems)
		{
			_ecsSystems = new EcsSystems(ecsWorlds.DefaultWorld);
			_ecsSystems.AddWorld(ecsWorlds.EventsWorld, GameConstants.EVENTS_WORLD_NAME);
			
			systems.ForEach(system => _ecsSystems.Add(system));
		}
		
		public void Initialize()
		{
			_ecsSystems.Init();
		}

		public void Tick()
		{
			_ecsSystems.Run();
		}

		public void Dispose()
		{
			_ecsSystems.GetWorld().Destroy();
			foreach (var world in _ecsSystems.GetAllNamedWorlds().Values)
			{
				world.Destroy();
			}
			
			_ecsSystems.Destroy();
		}
	}
}