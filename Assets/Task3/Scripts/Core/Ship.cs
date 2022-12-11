using System;
using System.Collections;
using UnityEngine;

namespace Task3.Core
{
	public class Ship : MonoBehaviour
	{
		public event Action StatsUpdated;

		[SerializeField] private ShipConfig config;
		[SerializeField] private Ship enemy;

		public ShipConfig Config => config;
		public bool IsAlive => Health > 0;

		public float Health { get; private set; }
		public float Shield { get; private set; }
		public float ShieldRegen { get; private set; }

		private ShipGun[] _gunModules;
		private ShipUpgrade[] _upgradeModules;

		private GameManager _gm;

		private Coroutine _regenShieldCoroutine;

		private void Awake()
		{
			_gm = GameManager.Instance;
		}

		private void OnEnable()
		{
			_gm.GameStarted.AddListener(HandleGameStart);
		}

		private void OnDisable()
		{
			_gm.GameStarted.RemoveListener(HandleGameStart);
		}

		private void Start()
		{
			Initialize();
		}

		private void Initialize()
		{
			Health = config.health;
			Shield = config.shield;
			ShieldRegen = config.shieldRegen;
			StatsUpdated?.Invoke();

			_gunModules = new ShipGun[config.gunSlots];
			_upgradeModules = new ShipUpgrade[config.upgradeSlots];
		}

		private void HandleGameStart()
		{
			foreach (var upgrade in _upgradeModules)
			{
				if (upgrade == null)
					continue;

				foreach (var gun in _gunModules)
				{
					if (gun == null)
						continue;

					gun.ApplyUpgrade(upgrade.Config);
				}
			}
			StatsUpdated?.Invoke();

			foreach (var gun in _gunModules)
			{
				if (gun == null)
					continue;

				gun.AttackTarget();
			}

			_regenShieldCoroutine = StartCoroutine(RegenShield());
		}

		private IEnumerator RegenShield()
		{
			while (IsAlive)
			{
				Shield = Mathf.Clamp(Shield + 1, 0, Config.shield);
				StatsUpdated?.Invoke();
				yield return new WaitForSeconds(ShieldRegen);
			}
		}

		private void ApplyUpgrade(ShipUpgradeConfig upgradeConfig)
		{
			Health += upgradeConfig.additionalHealth;
			Shield += upgradeConfig.additionalShield;
			ShieldRegen += upgradeConfig.additionalShieldRegen;
			StatsUpdated?.Invoke();
		}

		private void RemoveUpgrade(ShipUpgradeConfig upgradeConfig)
		{
			Health -= upgradeConfig.additionalHealth;
			Shield -= upgradeConfig.additionalShield;
			ShieldRegen -= upgradeConfig.additionalShieldRegen;
			StatsUpdated?.Invoke();
		}

		public void EquipGunModule(ShipGunConfig gunConfig, int slot)
		{
			Debug.Assert(slot >= 0 && slot < _gunModules.Length);
			Debug.Assert(_gunModules[slot] == null && gunConfig != null);
			_gunModules[slot] = Instantiate(gunConfig.prefab, transform);
			_gunModules[slot].enabled = false;
			_gunModules[slot].Initialize(gunConfig, enemy);
		}

		public void EquipUpgradeModule(ShipUpgradeConfig upgradeConfig, int slot)
		{
			Debug.Assert(slot >= 0 && slot < _upgradeModules.Length);
			Debug.Assert(_upgradeModules[slot] == null && upgradeConfig != null);
			_upgradeModules[slot] = Instantiate(upgradeConfig.prefab, transform);
			_upgradeModules[slot].Initialize(upgradeConfig);
			ApplyUpgrade(upgradeConfig);
		}

		public void UnequipGunModule(ShipGunConfig gunConfig, int slot)
		{
			Debug.Assert(slot >= 0 && slot < _gunModules.Length);
			Debug.Assert(gunConfig != null && _gunModules[slot].Config == gunConfig);
			Destroy(_gunModules[slot].gameObject);
			_gunModules[slot] = null;
		}

		public void UnequipUpgradeModule(ShipUpgradeConfig upgradeConfig, int slot)
		{
			Debug.Assert(slot >= 0 && slot < _upgradeModules.Length);
			Debug.Assert(upgradeConfig != null && _upgradeModules[slot].Config == upgradeConfig);
			RemoveUpgrade(_upgradeModules[slot].Config);
			Destroy(_upgradeModules[slot].gameObject);
			_upgradeModules[slot] = null;
		}

		public void TakeDamage(float damage)
		{
			if (!IsAlive)
				return;

			if (Shield > 0)
			{
				if (Shield >= damage)
				{
					Shield -= damage;
					StatsUpdated?.Invoke();
					return;
				}

				damage -= Shield;
			}

			Health -= damage;
			if (!IsAlive)
			{
				for (var i = 0; i < _gunModules.Length; i++)
				{
					if (_gunModules[i] == null)
						continue;

					Destroy(_gunModules[i]);
					_gunModules[i] = null;
				}

				StopCoroutine(_regenShieldCoroutine);
				_regenShieldCoroutine = null;

				_gm.RequestGameFinish(enemy);
			}
			StatsUpdated?.Invoke();
		}
	}
}