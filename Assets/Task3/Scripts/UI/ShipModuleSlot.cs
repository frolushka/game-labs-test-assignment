using Task3.Core;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Task3.UI
{
	public abstract class ShipModuleSlot : MonoBehaviour, IPointerClickHandler
	{
		public int SlotIndex { get; private set; }

		public void Initialize(int slotIndex)
		{
			SlotIndex = slotIndex;
		}

		void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
		{
			if (transform.childCount == 0)
				return;

			transform.GetChild(0).GetComponent<ShipModule>().Unequip(this);
		}

		public virtual bool CanEquip(ShipModuleConfigBase moduleConfig)
		{
			return transform.childCount == 0 && CanEquipInternal(moduleConfig);
		}

		protected abstract bool CanEquipInternal(ShipModuleConfigBase moduleConfig);
	}
}