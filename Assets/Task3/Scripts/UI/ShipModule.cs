using Task3.Core;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Task3.UI
{
	[RequireComponent(typeof(Image))]
	public abstract class ShipModule<TConfig> : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
		where TConfig : ShipModuleConfig
	{
		[SerializeField] private TextMeshProUGUI nameLabel;

		private Image _image;
		private RectTransform _rectTransform;
		private Vector2 _dragOffset;

		private Transform _defaultParent;

		private TConfig _config;
		public TConfig Config => _config;

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
				if (hovered.TryGetComponent<ShipModuleSlot<TConfig>>(out var slot)
					&& slot.CanEquip(_config))
				{
					AttachToSlot(slot.transform);
					slot.TryEquip(_config);
					return;
				}
			}

			ReturnToDefaultParent();
		}

		private void ReturnToDefaultParent()
		{
			_rectTransform.SetParent(null);
			_rectTransform.SetParent(_defaultParent);
			_image.raycastTarget = true;
		}

		public void Initialize(TConfig moduleConfig)
		{
			_config = moduleConfig;
			nameLabel.text = moduleConfig.moduleName;
		}

		public void AttachToSlot(Transform slotTransform)
		{
			_rectTransform.SetParent(slotTransform);
			_rectTransform.localPosition = Vector2.zero;
			_image.raycastTarget = false;
		}

		public void RemoveAttachment()
		{
			ReturnToDefaultParent();
			_image.raycastTarget = true;
		}
	}
}