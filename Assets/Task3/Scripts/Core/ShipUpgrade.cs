using UnityEngine;

namespace Task3.Core
{
	public class ShipUpgrade : MonoBehaviour
	{
		public ShipUpgradeConfig Config { get; private set; }

		public void Initialize(ShipUpgradeConfig config)
		{
			Config = config;
		}
	}
}