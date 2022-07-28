using UnityEngine;

namespace Game.Components
{
	public struct DoorComponent
	{
		public string Id;
		public Vector3 ActivatorPosition;
		public bool Activated;
		public float OpeningProgress;
	}
}