using UnityEngine;

namespace Game.Core
{
	public static class GameConstants
	{
		public static readonly string EVENTS_WORLD_NAME = "Events";
		public const float DOOR_ACTIVATOR_RADIUS = 0.5f;
		public const float DOOR_OPENING_PROGRESS_SPEED = 0.5f;
		public const float SQR_DISTANCE_MIN_THRESHOLD = 0.00001f;
		public const float PLAYER_MOVE_SPEED = 3f;

		public static readonly int GROUND_HIT_MASK = LayerMask.GetMask("Ground");
	}
}