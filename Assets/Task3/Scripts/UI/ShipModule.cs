using System;
using Task3.Core;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Task3.UI
{
	[RequireComponent(typeof(Image))]
	public class ShipModule : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
	{
		public event Action<ShipModuleConfigBase, int> Equipped;
		public event Action<ShipModuleConfigBase, int> Unequipped;

		[SerializeField] private TextMeshProUGUI nameLabel;

		private Image _image;
		private RectTransform _rectTransform;
		private Vector2 _dragOffset;

		private Transform _defaultParent;

		private ShipModuleConfigBase _config;
		private int _groupIndex;

		private void Awake()
		{
			_image = GetComponent<Image>();
			_rectTransform = transform as RectTransform;
		}

		private void Start()
		{
			_defaultParent = _rectTransform.parent;
		}

		void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
		{
			_dragOffset = eventData.position - (Vector2)Input.mousePosition;
			_image.raycastTarget = false;
		}

		void IDragHandler.OnDrag(PointerEventData eventData)
		{
			_rectTransform.position = eventData.position + _dragOffset;
		}

		void IEndDragHandler.OnEndDrag(PointerEventData eventData)
		{
			foreach (var hovered in eventData.hovered)
			{
				if (hovered.TryGetComponent<ShipModuleSlot>(out var slot)
					&& slot.CanEquip(_config))
				{
					Equip(slot);
					return;
				}
			}

			ReturnToDefaultParent();
		}

		private void ReturnToDefaultParent()
		{
			_rectTransform.SetParent(null);
			_rectTransform.SetParent(_defaultParent);
			_rectTransform.SetSiblingIndex(_groupIndex);
			_image.raycastTarget = true;
		}

		public void Initialize(ShipModuleConfigBase moduleConfig, int groupIndex)
		{
			_config = moduleConfig;

			_groupIndex = groupIndex;
			nameLabel.text = moduleConfig.moduleName;
		}

		public void Equip(ShipModuleSlot slot)
		{
			_rectTransform.SetParent(slot.transform);
			_rectTransform.localPosition = Vector2.zero;
			_image.raycastTarget = false;
			Equipped?.Invoke(_config, slot.SlotIndex);
		}

		public void Unequip(ShipModuleSlot slot)
		{
			ReturnToDefaultParent();
			_image.raycastTarget = true;
			Unequipped?.Invoke(_config, slot.SlotIndex);
		}
	}
}