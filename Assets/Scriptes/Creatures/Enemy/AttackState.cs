using System;
using Scriptes.Creatures.Hero;
using Scriptes.Creatures.StateMachine;
using UnityEngine;

namespace Scriptes.Creatures
{
    [RequireComponent(typeof(Animator))]
    public class AttackState : State
    {
        [SerializeField] private int _damage;
        [SerializeField] private float _delay;
        
        private float _lastAttackTime;
        private Animator _animator;

        private void Start()
        {
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            if (_lastAttackTime <= 0)
            {
               Attack(Target);
               _lastAttackTime = _delay;
            }

            _lastAttackTime -= Time.deltaTime;
        }

        private void Attack(Player target)
        {
            _animator.Play("_attack");
            target.ApplyDamage(_damage);
        }
    }
}