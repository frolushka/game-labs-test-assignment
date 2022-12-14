using Task3.Core;
using TMPro;
using UnityEngine;

namespace Task3.UI
{
	public class ShipPanel : MonoBehaviour
	{
		[SerializeField] private ShipGunSlot gunSlotPrefab;
		[SerializeField] private ShipUpgradeSlot upgradeSlotPrefab;

		[SerializeField] private Transform gunSlotsParent;
		[SerializeField] private Transform upgradeSlotsParent;

		[SerializeField] private TextMeshProUGUI healthLabel;
		[SerializeField] private TextMeshProUGUI shieldLabel;
		[SerializeField] private TextMeshProUGUI shieldRegenLabel;

		[SerializeField] private Ship ship;

		private void Start()
		{
			for (var i = 0; i < ship.Config.gunSlots; i++)
			{
				var gunSlot = Instantiate(gunSlotPrefab, gunSlotsParent);
				gunSlot.Initialize(ship, i);
			}

			for (var i = 0; i < ship.Config.upgradeSlots; i++)
			{
				var upgradeSlot = Instantiate(upgradeSlotPrefab, upgradeSlotsParent);
				upgradeSlot.Initialize(ship, i);
			}

			ship.StatsUpdated += HandleStatsUpdate;
		}

		private void HandleStatsUpdate()
		{
			healthLabel.text = $"Health: {(int)ship.Health}";
			shieldLabel.text = $"Shield: {(int)ship.Shield}";
			shieldRegenLabel.text = $"Shield regen: {(int)ship.ShieldRegen}";
		}
	}
}