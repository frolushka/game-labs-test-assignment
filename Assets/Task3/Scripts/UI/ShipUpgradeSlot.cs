using Task3.Core;

namespace Task3.UI
{
	public class ShipUpgradeSlot : ShipModuleSlot<ShipUpgradeConfig>
	{
		public override void TryEquip(ShipUpgradeConfig config)
		{
			_ship.EquipUpgradeModule(config, _slotIndex);
		}

		protected override void TryUnequip(ShipUpgradeConfig config)
		{
			_ship.UnequipUpgradeModule(config, _slotIndex);
		}
	}
}