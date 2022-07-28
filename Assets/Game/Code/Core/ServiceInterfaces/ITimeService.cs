namespace Game.Services
{
	public interface ITimeService
	{
		float Time { get; }
		float DeltaTime { get; }
	}
}