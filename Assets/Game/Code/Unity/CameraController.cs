using UnityEngine;
using Zenject;

namespace Game.Unity
{
	public class CameraController : MonoBehaviour
	{
		private Transform _playerTransform;
		private Vector3 _positionOffset;
		
		[Inject]
		public void Construct(PlayerView playerView)
		{
			_playerTransform = playerView.transform;
			_positionOffset = transform.position - _playerTransform.position;
		}

		private void Update()
		{
			if (_playerTransform == null)
			{
				return;
			}

			transform.position = _playerTransform.position + _positionOffset;
		}
	}
}