using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Unity
{
	public class SceneDoorsService
	{
		private readonly Dictionary<string, DoorView> _doors;

		public SceneDoorsService(IReadOnlyList<DoorView> doorViews)
		{
			_doors = doorViews.ToDictionary(door => door.Id);
		}

		public void AssignDoorViewToEntity(string doorId, int entity)
		{
			var doorView = GetDoorView(doorId);
			doorView?.AssignToEntity(entity);
		}

		private DoorView GetDoorView(string id)
		{
			if (!_doors.TryGetValue(id, out var doorView))
			{
				Debug.LogError($"Cannot find view for door id: {id}");
			}

			return doorView;
		}
	}
}