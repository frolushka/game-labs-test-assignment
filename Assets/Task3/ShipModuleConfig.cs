using UnityEngine;

namespace Task3
{
	public abstract class ShipModuleConfig : ScriptableObject
	{
		public abstract void Equip(Ship ship);
		public abstract void Unequip(Ship ship);
	}
}