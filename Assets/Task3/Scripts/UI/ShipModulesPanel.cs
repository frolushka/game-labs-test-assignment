using Task3.Core;
using UnityEngine;

namespace Task3.UI
{
	public class ShipModulesPanel : MonoBehaviour
	{
		[SerializeField] private ShipModule shipGunPrefab;
		[SerializeField] private ShipModule shipUpgradePrefab;
		[SerializeField] private Ship ship;

		private void Start()
		{
			// TODO rework method
			ShipModule module;
			for (var i = 0; i < ship.Config.suitableModuleConfigs.Length; i++)
			{
				var moduleConfig = ship.Config.suitableModuleConfigs[i];
				switch (moduleConfig)
				{
					case ShipGunConfig gunConfig:
						module = Instantiate(shipGunPrefab, transform);
						module.Initialize(gunConfig, i);
						module.Equipped += HandleEquip;
						module.Unequipped += HandleUnequip;
						break;
					case ShipUpgradeConfig upgradeConfig:
						module = Instantiate(shipUpgradePrefab, transform);
						module.Initialize(upgradeConfig, i);
						module.Equipped += HandleEquip;
						module.Unequipped += HandleUnequip;
						break;
				}
			}
		}

		private void HandleEquip(ShipModuleConfigBase config, int slotIndex)
		{
			switch (config)
			{
				case ShipGunConfig gunConfig:
					ship.EquipGunModule(gunConfig, slotIndex);
					break;
				case ShipUpgradeConfig upgradeConfig:
					ship.EquipUpgradeModule(upgradeConfig, slotIndex);
					break;
			}
		}

		private void HandleUnequip(ShipModuleConfigBase config, int slotIndex)
		{
			switch (config)
			{
				case ShipGunConfig gunConfig:
					ship.UnequipGunModule(gunConfig, slotIndex);
					break;
				case ShipUpgradeConfig upgradeConfig:
					ship.UnequipUpgradeModule(upgradeConfig, slotIndex);
					break;
			}
		}
	}
}