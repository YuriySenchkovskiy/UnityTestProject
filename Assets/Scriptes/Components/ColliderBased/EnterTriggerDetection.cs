using System;
using UnityEngine;

namespace Components.ColliderBased
{
    public class EnterTriggerDetection : EnterTriggerComponent
    {
        public static event Action TriggerDetected;

        protected override void OnTriggerEnter2D(Collider2D other)
        {
            base.OnTriggerEnter2D(other);
            TriggerDetected?.Invoke();
        }
    }
}