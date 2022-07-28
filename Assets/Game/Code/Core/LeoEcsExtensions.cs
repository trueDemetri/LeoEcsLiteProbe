using Leopotam.EcsLite;

namespace Game.Core
{
	public static class LeoEcsExtensions
	{
		public static ref TComponent Refresh<TComponent>(this EcsPool<TComponent> pool, int entity) where TComponent : struct
		{
			return ref pool.Has(entity) ? ref pool.Get(entity) : ref pool.Add(entity);
		}
	}
}