using UnityEngine;

namespace Task3.Core
{
	[CreateAssetMenu(menuName = "Task3/Ship Module/Ship Upgrade Config")]
	public class ShipUpgradeConfig : ShipModuleConfig<ShipUpgrade>
	{
		public float additionalHealth;
		public float additionalShield;
		[Range(0, 1)]
		public float additionalShieldRegen;
		[Range(0, 1)]
		public float reloadDelayBonus;
	}
}