using UnityEngine;

namespace Task3.Core
{
	[CreateAssetMenu(menuName = "Task3/Ship Config")]
	public class ShipConfig : ScriptableObject
	{
		public string shipName;

		public float health;
		public float shield;
		public float shieldRegen;

		public int gunSlots;
		public int upgradeSlots;

		public ShipModuleConfigBase[] suitableModuleConfigs;
	}
}