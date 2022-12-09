using UnityEngine;

namespace Task3
{
	public class Ship : MonoBehaviour
	{
		[SerializeField] private ShipConfig config;
		public ShipConfig Config => config;
	}
}