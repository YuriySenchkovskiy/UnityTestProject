using System;
using UnityEngine;

namespace Components.ColliderBased
{
    public class EnterTriggerDetection : EnterTriggerComponent
    {
        [SerializeField] private Transform _targetTransform;
        
        public static event Action<Transform> TriggerDetected;

        protected override void OnTriggerEnter2D(Collider2D other)
        {
            base.OnTriggerEnter2D(other);
            TriggerDetected?.Invoke(_targetTransform);
        }
    }
}