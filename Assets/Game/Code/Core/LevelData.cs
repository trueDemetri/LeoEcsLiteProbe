using System.Collections.Generic;
using UnityEngine;

namespace Game.Core
{
	public class LevelData
	{
		public IReadOnlyList<DoorData> Doors { get; }
		public Vector3 PlayerSpawnPosition { get; }

		public LevelData(DoorData[] doors, Vector3 playerSpawnPosition)
		{
			PlayerSpawnPosition = playerSpawnPosition;
			Doors = doors;
		}
	}
}