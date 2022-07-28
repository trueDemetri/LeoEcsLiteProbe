using UnityEngine;

namespace Game.Core
{
	public class DoorData
	{
		public readonly string Id;
		public Vector3 ActivatorPosition;

		public DoorData(string id, Vector3 activatorPosition)
		{
			Id = id;
			ActivatorPosition = activatorPosition;
		}
	}
}