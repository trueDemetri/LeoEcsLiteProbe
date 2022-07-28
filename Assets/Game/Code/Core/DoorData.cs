using UnityEngine;

namespace Game.Core
{
	public class DoorData
	{
		public readonly string ConfigId;
		public Vector3 ActivatorPosition;

		public DoorData(string configId, Vector3 activatorPosition)
		{
			ConfigId = configId;
			ActivatorPosition = activatorPosition;
		}
	}
}