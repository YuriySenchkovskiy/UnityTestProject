using Components.ColliderBased;

namespace Components.GoBased
{
    public class RemoteSpawner : SpawnComponent
    {
        private void OnEnable()
        {
            EnterTriggerDetection.TriggerDetected += OnTriggerDetected;
        }

        private void OnDisable()
        {
            EnterTriggerDetection.TriggerDetected -= OnTriggerDetected;
        }
        
        private void OnTriggerDetected()
        {
            Spawn();
        }
    }
}