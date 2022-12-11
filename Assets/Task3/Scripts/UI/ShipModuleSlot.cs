using Task3.Core;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Task3.UI
{
	public abstract class ShipModuleSlot<TConfig> : MonoBehaviour, IPointerClickHandler
		where TConfig : ShipModuleConfig
	{
		protected Ship _ship;
		protected int _slotIndex;

		public void Initialize(Ship ship, int slotIndex)
		{
			_ship = ship;
			_slotIndex = slotIndex;
		}

		void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
		{
			if (transform.childCount == 0)
				return;

			// TODO I don't like this.
			var module = transform.GetChild(0).GetComponent<ShipModule<TConfig>>();
			TryUnequip(module.Config);
			module.RemoveAttachment();
		}

		public bool CanEquip(TConfig moduleConfig)
		{
			return transform.childCount == 0;
		}

		public abstract void TryEquip(TConfig config);

		protected abstract void TryUnequip(TConfig config);
	}
}