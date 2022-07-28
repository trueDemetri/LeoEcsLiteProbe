using UnityEngine;
using Zenject;

namespace Game.Unity
{
	public abstract class EntityView : MonoBehaviour, ITickable
	{
		private TickableManager _tickableManager;
		
		[Inject]
		public void BaseConstruct(TickableManager tickableManager)
		{
			_tickableManager = tickableManager;
		}

		protected virtual void OnEnable()
		{
			_tickableManager.Add(this);			
		}
		
		public virtual void Tick()
		{}

		protected virtual void OnDisable()
		{
			DisableTicking();
		}

		private void DisableTicking()
		{
			_tickableManager.Remove(this);
		}
	}
}