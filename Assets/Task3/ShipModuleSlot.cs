using UnityEngine;
using UnityEngine.EventSystems;

namespace Task3
{
	public class ShipModuleSlot : MonoBehaviour, IPointerClickHandler
	{
		private ShipModuleConfig _config;

		public void OnPointerClick(PointerEventData eventData)
		{
			if (_config == null)
				return;

			_config = null;
		}
	}
}