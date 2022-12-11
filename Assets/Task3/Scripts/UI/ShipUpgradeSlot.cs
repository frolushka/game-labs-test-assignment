using Task3.Core;

namespace Task3.UI
{
	public class ShipUpgradeSlot : ShipModuleSlot
	{
		protected override bool CanEquipInternal(ShipModuleConfigBase moduleConfig)
		{
			return moduleConfig is ShipUpgradeConfig;
		}
	}
}