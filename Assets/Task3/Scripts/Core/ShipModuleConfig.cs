using UnityEngine;

namespace Task3.Core
{
	public abstract class ShipModuleConfig : ScriptableObject
	{
		public string moduleName;
	}

	public abstract class ShipModuleConfig<TPrefab> : ShipModuleConfig
		where TPrefab : MonoBehaviour
	{
		public TPrefab prefab;
	}
}