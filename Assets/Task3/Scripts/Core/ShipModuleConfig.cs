using UnityEngine;

namespace Task3.Core
{
	public abstract class ShipModuleConfigBase : ScriptableObject
	{
		public string moduleName;
	}

	public abstract class ShipModuleConfig<TPrefab> : ShipModuleConfigBase
		where TPrefab : MonoBehaviour
	{
		public TPrefab prefab;
	}
}