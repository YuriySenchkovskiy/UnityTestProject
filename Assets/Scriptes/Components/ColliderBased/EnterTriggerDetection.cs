using System;
using UnityEngine;

namespace Components.ColliderBased
{
    public class EnterTriggerDetection : EnterTriggerComponent
    {
        [SerializeField] private Transform _targetTransform;
        
        public static Action<Transform> TriggerTransformDetected;
        public static Action TriggerDetected;

        protected override void OnTriggerEnter2D(Collider2D other)
        {
            base.OnTriggerEnter2D(other);
            TriggerTransformDetected?.Invoke(_targetTransform);
            //TriggerDetected?.Invoke();
        }
    }
}