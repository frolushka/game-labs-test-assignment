using Task3.Core;
using UnityEngine;

namespace Task3.UI
{
	public class ShipModulesPanel : MonoBehaviour
	{
		[SerializeField] private ShipGunModule shipGunPrefab;
		[SerializeField] private ShipUpgradeModule shipUpgradePrefab;
		[SerializeField] private Ship ship;

		private void Start()
		{
			InitializeModules(ship.Config.suitableGunConfigs, shipGunPrefab);
			InitializeModules(ship.Config.suitableUpgradeConfigs, shipUpgradePrefab);
		}

		private void InitializeModules<TConfig>(TConfig[] configs, ShipModule<TConfig> prefab)
			where TConfig : ShipModuleConfig
		{
			foreach (var config in configs)
			{
				var module = Instantiate(prefab, transform);
				module.Initialize(config);
			}
		}
	}
}