using Game.Services;

namespace Game.Unity
{
	public class UnityTimeService : ITimeService
	{
		public float Time => UnityEngine.Time.time;
		public float DeltaTime => UnityEngine.Time.deltaTime;
	}
}