using System;
using System.Collections.Generic;
using UnityEngine;

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
                
            }
        }
    }
}