using System;
using Scriptes.Creatures.StateMachine;
using UnityEngine;

namespace Scriptes.Creatures
{
    [RequireComponent(typeof(Animator))]
    public class MoveState : State
    {
        [SerializeField] private float _speed;

        private Animator _animator;

        private void Start()
        {
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            transform.position =
                Vector2.MoveTowards(transform.position, Target.transform.position, _speed * Time.deltaTime);
        }
    }
}