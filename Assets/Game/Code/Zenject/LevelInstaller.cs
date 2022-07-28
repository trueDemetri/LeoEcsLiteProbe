using System.Linq;
using Game.Core;
using Game.Services;
using Game.Systems;
using Game.Systems.IntermediateSystems;
using Game.Unity;
using Leopotam.EcsLite;
using UnityEngine;
using Zenject;

namespace Game.Zenject
{
	public class LevelInstaller : MonoInstaller
	{
		[SerializeField] private Camera _mainCamera;
		[SerializeField] private DoorView[] _doorViews;
		[SerializeField] private PlayerView _playerView;

		private EcsWorld _eventsWorld;

		public override void InstallBindings()
		{
			InstallData();
			InstallEcsWorlds();
			InstallEcsSystems();
			InstallEcsTools();

			IntegrateEcsToUnity();
			
			InstallUnityServices();
		}

		private void InstallEcsWorlds()
		{
			var defaultWorld = new EcsWorld();
			_eventsWorld = new EcsWorld();

			Container.Bind<EcsWorlds>().FromInstance(new EcsWorlds(defaultWorld, _eventsWorld)).AsSingle();
		}
		
		private void InstallEcsSystems()
		{
			Container.BindEcsSystem<PlayerSpawnSystem>();
			Container.BindEcsSystem<DoorsSpawnSystem>();
			Container.BindEcsSystem<PlayerPointAndClickSystem>();
			
			Container.BindEcsSystem<MoveSystem>();
			Container.BindEcsSystem<DoorOpeningSystem>();
			Container.BindEcsSystem<PlayerMoveSystem>();
			
			Container.BindEcsSystem<DoorsShowingSystem>();
			
			Container.BindEcsSystem<EventsClearSystem>();
		}

		private void InstallEcsTools()
		{
			Container.Bind<EcsMessenger>().AsSingle().WithArguments(_eventsWorld);
		}

		private void IntegrateEcsToUnity()
		{
			Container.Bind<ITimeService>().To<UnityTimeService>().AsSingle();
			Container.BindInterfacesTo<EcsMotor>().AsSingle().NonLazy();
		}

		private void InstallData()
		{
			var buttonPositions = _doorViews.Select(GetDoorDataFromView).ToArray();
			var playerSpawnPosition = _playerView.transform.position;
			
			Container.Bind<LevelData>()
				.FromInstance(new LevelData(buttonPositions, playerSpawnPosition)).AsSingle();
		}

		private void InstallUnityServices()
		{
			Container.BindInstance(_mainCamera).AsSingle();
			Container.BindInstance(_playerView).AsSingle();
			Container.Bind<SceneDoorsService>().FromInstance(new SceneDoorsService(_doorViews)).AsSingle();
		}

		private static DoorData GetDoorDataFromView(DoorView doorView)
		{
			var doorData = new DoorData(doorView.Id, doorView.ActivatorPosition);
			return doorData;
		}
	}
}