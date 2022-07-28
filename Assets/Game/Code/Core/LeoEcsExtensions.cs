using Leopotam.EcsLite;

namespace Game.Core
{
	public static class LeoEcsExtensions
	{
		public static ref TComponent Refresh<TComponent>(this EcsPool<TComponent> pool, int entity) where TComponent : struct
		{
			return ref pool.Has(entity) ? ref pool.Get(entity) : ref pool.Add(entity);
		}

		public static bool GetSingleEntity(this EcsFilter filter, out int result)
		{
			var enumerator = filter.GetEnumerator();
			if (!enumerator.MoveNext())
			{
				result = -1;
				enumerator.Dispose();
				return false;
			}

			result = enumerator.Current;
			enumerator.Dispose();

			return true;
		}
	}
}