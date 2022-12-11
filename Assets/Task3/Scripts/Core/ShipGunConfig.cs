using UnityEngine;

namespace Task3.Core
{
	[CreateAssetMenu(menuName = "Task3/Ship Module/Ship Gun Config")]
	public class ShipGunConfig : ShipModuleConfig<ShipGun>
	{
		public float damage;
		public float reload;
	}
}