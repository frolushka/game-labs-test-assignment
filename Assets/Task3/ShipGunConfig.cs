using UnityEngine;

namespace Task3
{
	[CreateAssetMenu(menuName = "Task3/Ship Module/Ship Gun Config")]
	public class ShipGunConfig : ShipModuleConfig
	{
		public float damage;
		public float reload;

		public override void Equip(Ship ship)
		{
			throw new System.NotImplementedException();
		}

		public override void Unequip(Ship ship)
		{
			throw new System.NotImplementedException();
		}
	}
}