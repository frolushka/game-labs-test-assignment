using System;
using UnityEngine;

namespace Task3
{
	[CreateAssetMenu(menuName = "Task3/Ship Config")]
	public class ShipConfig : ScriptableObject
	{
		public float health;
		public float shield;
		public float shieldRegenTime;
		public int gunSlots;
		public int upgradeSlots;
	}
}