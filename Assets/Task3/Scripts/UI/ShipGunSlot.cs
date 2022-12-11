using Task3.Core;

namespace Task3.UI
{
	public class ShipGunSlot : ShipModuleSlot
	{
		protected override bool CanEquipInternal(ShipModuleConfigBase moduleConfig)
		{
			return moduleConfig is ShipGunConfig;
		}
	}
}