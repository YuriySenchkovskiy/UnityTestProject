using Components.ColliderBased;
using UnityEngine;

namespace Components.GoBased
{
    public class RemoteTransformSpawner : SpawnComponent
    {
        private void OnEnable()
        {
            EnterTriggerDetection.TriggerTransformDetected += OnTriggerTransformDetected;
        }

        private void OnDisable()
        {
            EnterTriggerDetection.TriggerTransformDetected -= OnTriggerTransformDetected;
        }
        
        private void OnTriggerTransformDetected(Transform target)
        {
            Target = target;
            Spawn();
        }
    }
}