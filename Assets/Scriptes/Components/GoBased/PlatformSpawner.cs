using Components.ColliderBased;
using UnityEngine;

namespace Components.GoBased
{
    public class PlatformSpawner : SpawnComponent
    {
        [SerializeField] private bool _isSpawnOnEnable;
        [SerializeField] private Transform _enablePlatform;
        
        private void OnEnable()
        {
            EnterTriggerDetection.TriggerDetected += OnTriggerDetected;
            
            if (_isSpawnOnEnable)
            {
                _target = _enablePlatform;
                SpawnInstance();
            }
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