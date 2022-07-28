using System;
using System.Linq;
using Game.Components;
using Leopotam.EcsLite;
using UnityEngine;
using Zenject;

namespace Game.Unity
{
	public class DoorView : MonoBehaviour
	{
		public string Id => _doorId ??= Guid.NewGuid().ToString();
		public Vector3 ActivatorPosition => _activationButtonTransform.position;
		
		[SerializeField] private Transform _activationButtonTransform;
		[SerializeField] private Transform _movingTransform;
		[SerializeField] private Vector3 _fullOffset;
		[SerializeField] private Color _inactiveColor;
		[SerializeField] private Color _activeColor;
		
		private string _doorId;
		private int? _entity;

		private EcsPool<DoorComponent> _doorsPool;

		private Vector3 _movingPartStartPoint;
		private Vector3 _movingPartStopPoint;
		private bool? _isActivated;
		
		private Renderer[] _renderers;

		[Inject]
		private void Construct(EcsWorlds ecsWorlds)
		{
			var world = ecsWorlds.DefaultWorld;
			_doorsPool = world.GetPool<DoorComponent>();
			
			CollectRenderers();
			CalcMovingPositions();
			SetActivationStatus(false);
		}

		private void Update()
		{
			ShowEntity();
		}

		public void AssignToEntity(int entity)
		{
			_entity = entity;
		}
		
		private void CollectRenderers()
		{
			_renderers = GetComponentsInChildren<Renderer>();
			if (!_activationButtonTransform.IsChildOf(transform))
			{
				_renderers = _renderers.Union(_activationButtonTransform.GetComponentsInChildren<Renderer>())
					.ToArray();
			}			
		}

		private void CalcMovingPositions()
		{
			_movingPartStartPoint = _movingTransform.position;
			_movingPartStopPoint = _movingTransform.TransformPoint(_fullOffset);
		}

		private void ShowEntity()
		{
			if (_entity == null)
			{
				return;
			}

			ref var door = ref _doorsPool.Get(_entity.Value);
			SetActivationStatus(door.Activated);
			SetOpenProgress(door.OpeningProgress);
		}
		
		private void SetActivationStatus(bool activated)
		{
			if (_isActivated != null && _isActivated.Value == activated)
			{
				return;
			}

			foreach (var rend in _renderers)
			{
				rend.material.color = activated ? _activeColor : _inactiveColor;
			}

			_isActivated = activated;
		}

		private void SetOpenProgress(float openProgress)
		{
			_movingTransform.position = Vector3.Lerp(_movingPartStartPoint, _movingPartStopPoint, openProgress);
		}
	}
}