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

        private const string Fire = "fire";
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
                _currentWeapon.SpriteAnimator.SetClip(Fire);
                _currentWeapon.Shoot(_shootPoint);
            }
        }
    }
}