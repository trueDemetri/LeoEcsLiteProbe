using Leopotam.EcsLite;
using Zenject;

namespace Game.Zenject
{
	public static class ZenjectExtensions
	{
		public static void BindEcsSystem<T>(this DiContainer container) where T : IEcsSystem
		{
			container.Bind<IEcsSystem>().To<T>().AsSingle();
		}
	}
}