using Task3.Core;

namespace Task3.UI
{
	public class ShipGunSlot : ShipModuleSlot<ShipGunConfig>
	{
		public override void TryEquip(ShipGunConfig config)
		{
			_ship.EquipGunModule(config, _slotIndex);
		}

		protected override void TryUnequip(ShipGunConfig config)
		{
			_ship.UnequipGunModule(config, _slotIndex);
		}
	}
}