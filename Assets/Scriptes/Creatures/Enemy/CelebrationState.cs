using System;
using Scriptes.Creatures.StateMachine;
using UnityEngine;

namespace Scriptes.Creatures
{
    [RequireComponent(typeof(Animator))]
    public class CelebrationState : State
    {
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void OnEnable()
        {
            _animator.Play("_idle");
        }

        private void OnDisable()
        {
            _animator.StopPlayback();
        }
    }
}