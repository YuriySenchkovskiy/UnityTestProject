using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Scriptes.Creatures.StateMachine.Transitions
{
    public class DistanceTransition : Transition
    {
        [SerializeField] private float _transitionRange;
        [SerializeField] private float _rangeSpread;

        private void Start()
        {
            _transitionRange += Random.Range(-_rangeSpread, _rangeSpread);
        }

        private void Update()
        {
            if (Vector2.Distance(transform.position, Target.transform.position) < _transitionRange)
            {
                NeedTransit = true;
            }
        }
    }
}