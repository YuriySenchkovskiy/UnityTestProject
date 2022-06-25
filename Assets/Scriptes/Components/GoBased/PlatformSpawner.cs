using Components.ColliderBased;
using UnityEngine;

namespace Components.GoBased
{
    public class PlatformSpawner : SpawnComponent
    {
        private void OnEnable()
        {
            EnterTriggerDetection.TriggerDetected += OnTriggerDetected;
        }

        private void OnDisable()
        {
            EnterTriggerDetection.TriggerDetected -= OnTriggerDetected;
        }
        
        private void OnTriggerDetected(Transform target)
        {
            _target = target;
            SpawnInstance();
        }
    }
}