using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scriptes.Creatures.Hero
{
    [RequireComponent(typeof(Animator))]
    public class Player : MonoBehaviour
    {
        [SerializeField] private int _health;
        [SerializeField] private List<Weapon.Weapon> _weapons;
        [SerializeField] private Transform _shootPoint;
        
        private Weapon.Weapon _currentWeapon;
        private int _currentHealth;
        private Animator _animator;

        public int Money { get; private set; }

        public event Action<int, int> HealthChanged;
        public event Action<int> MoneyChanged;

        private void Start()
        {
            _currentWeapon = _weapons[0];
            _currentHealth = _health;
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _currentWeapon.Shoot(_shootPoint);
            }
        }

        public void AddMoney(int reward)
        {
            Money += reward;
            MoneyChanged?.Invoke(Money);
        }

        public void BuyWeapon(Weapon.Weapon weapon)
        {
            Money -= weapon.Price;
            _weapons.Add(weapon);
            MoneyChanged?.Invoke(Money);
        }

        public void ApplyDamage(int damage)
        {
            _currentHealth -= damage;
            HealthChanged?.Invoke(_currentHealth, _health);

            if (_currentHealth <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}