using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace Scriptes.Creatures.Hero
{
    [RequireComponent(typeof(Animator))]
    public class Player : MonoBehaviour
    {
        [SerializeField] private int _health;
        [SerializeField] private List<Weapon.Weapon> _weapons;
        [SerializeField] private Transform _weaponPoint;
        
        private int _currentWeaponNumber;
        private int _currentHealth;

        public int Money { get; private set; }

        public event Action<int, int> HealthChanged;
        public event Action<int> MoneyChanged;

        private void Start()
        {
            CreateWeapon(_weapons[_currentWeaponNumber]);
            _currentHealth = _health;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                var go = GetComponentInChildren<Weapon.Weapon>();
                go.Shoot();
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

        public void NextWeapon()
        {
            TurnOffWeapon();
            
            if (_currentWeaponNumber == _weapons.Count - 1)
            {
                _currentWeaponNumber = 0;
            }
            else
            {
                _currentWeaponNumber++;
            }
            
            CreateWeapon(_weapons[_currentWeaponNumber]);
        }
        
        public void PreviousWeapon()
        {
            TurnOffWeapon();
            
            if (_currentWeaponNumber == 0)
            {
                _currentWeaponNumber = _weapons.Count - 1;
            }
            else
            {
                _currentWeaponNumber--;
            }
            
            CreateWeapon(_weapons[_currentWeaponNumber]);
        }

        private void TurnOffWeapon()
        {
            var go = GetComponentInChildren<Weapon.Weapon>();
            Destroy(go.gameObject);
        }

        private void CreateWeapon(Weapon.Weapon weapon)
        {
            Instantiate(weapon, 
                _weaponPoint.position, 
                quaternion.identity,
                _weaponPoint.transform);
        }
    }
}