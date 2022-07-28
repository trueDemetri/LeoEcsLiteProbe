using UnityEngine;

namespace Game.Components
{
	public struct PlayerMoveRequest
	{
		public Vector3 TargetPosition;

		public PlayerMoveRequest(Vector3 targetPosition)
		{
			TargetPosition = targetPosition;
		}
	}
}