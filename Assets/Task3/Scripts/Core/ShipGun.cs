using System.Collections;
using UnityEngine;

namespace Task3.Core
{
	public class ShipGun : MonoBehaviour
	{
		[SerializeField] private GameObject hitViewPrefab;

		public ShipGunConfig Config { get; private set; }

		private Ship _target;

		private float _reload;
		private float _damage;

		public void Initialize(ShipGunConfig config, Ship target)
		{
			Config = config;
			_target = target;
			_reload = config.reload;
			_damage = config.damage;
		}

		public void ApplyUpgrade(ShipUpgradeConfig module)
		{
			_reload *= 1 - module.reloadDelayBonus;
		}

		public void AttackTarget()
		{
			StartCoroutine(AttackTarget(_target));
		}

		private IEnumerator AttackTarget(Ship target)
		{
			// NOTE hotfix for oneshot nre
			yield return null;
			while (target.IsAlive)
			{
				target.TakeDamage(_damage);
				// TODO
				var hitView = Instantiate(hitViewPrefab, target.transform);
				hitView.transform.localPosition = Random.insideUnitSphere;
				Destroy(hitView, 1);
				// Allocation, but rare.
				yield return new WaitForSeconds(_reload);
			}
		}
	}
}