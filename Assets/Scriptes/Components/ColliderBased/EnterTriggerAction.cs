using System;
using UnityEngine;
using Utils;

namespace Components.ColliderBased
{
    public class EnterTriggerAction : MonoBehaviour
    {
        [SerializeField] private LayerMask _layer = ~0;
        [SerializeField] private EnterEventComponent _action; 
        [SerializeField] private Transform _targetTransform;
        
        public static Action<Transform> TriggerTransformDetected;
        public static Action TriggerDetected;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.gameObject.GetLayerStatus(_layer))
            {
                return;
            }

            _action?.Invoke(other.gameObject);
            TriggerTransformDetected?.Invoke(_targetTransform);
            TriggerDetected?.Invoke();
        }
    }
}