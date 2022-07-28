using Game.Components;
using Game.Core;
using Leopotam.EcsLite;
using UnityEngine;

namespace Game.Systems.IntermediateSystems
{
	public class TimeSystem : IEcsInitSystem, IEcsRunSystem
	{
		private int _timerEntity;
		private EcsPool<TimeComponent> _pool;

		public void Init(IEcsSystems systems)
		{
			var world = systems.GetWorld();
			_pool = world.GetPool<TimeComponent>();
			
			_timerEntity = world.NewEntity();
			_pool.Add(_timerEntity);
		}

		public void Run(IEcsSystems systems)
		{
			ref var timeComponent = ref _pool.Get(_timerEntity);
			timeComponent.Time = Time.time;
			timeComponent.DeltaTime = Time.deltaTime;
		}
	}
}