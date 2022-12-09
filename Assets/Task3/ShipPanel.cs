using System;
using UnityEngine;

namespace Task3
{
	[RequireComponent(typeof(CanvasGroup))]
	public class ShipPanel : MonoBehaviour
	{
		[SerializeField] private ShipModuleSlot moduleSlotPrefab;
		[SerializeField] private Transform gunSlotsParent;
		[SerializeField] private Transform upgradeSlotsParent;
		[SerializeField] private Ship ship;

		private CanvasGroup _canvasGroup;

		private void Awake()
		{
			_canvasGroup = GetComponent<CanvasGroup>();
		}

		private void OnEnable()
		{
			GameManager.Instance.GameStarted.AddListener(HandleGameStart);
		}

		private void OnDisable()
		{
			GameManager.Instance.GameStarted.RemoveListener(HandleGameStart);
		}

		private void Start()
		{
			for (var i = 0; i < ship.Config.gunSlots; i++)
				Instantiate(moduleSlotPrefab, gunSlotsParent);
			for (var i = 0; i < ship.Config.upgradeSlots; i++)
				Instantiate(moduleSlotPrefab, upgradeSlotsParent);
		}

		private void HandleGameStart()
		{
			_canvasGroup.interactable = false;
		}
	}
}