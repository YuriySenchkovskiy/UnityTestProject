using Components.ColliderBased;
using UnityEngine;

namespace Components.GoBased
{
    public class RemoteTransformSpawner : SpawnComponent
    {
        private void OnEnable()
        {
            EnterTriggerAction.TriggerTransformDetected += OnTriggerTransformDetected;
        }

        private void OnDisable()
        {
            EnterTriggerAction.TriggerTransformDetected -= OnTriggerTransformDetected;
        }
        
        private void OnTriggerTransformDetected(Transform target)
        {
            Target = target;
            Spawn();
        }
    }
}