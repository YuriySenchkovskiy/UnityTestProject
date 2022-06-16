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

        private void OnEnemyDied(int reward)
        {
            Money += reward;
        }

        public void ApplyDamage(int damage)
        {
            _currentHealth -= damage;

            if (_currentHealth <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}