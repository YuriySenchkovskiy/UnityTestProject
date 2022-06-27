using Components.ColliderBased;

namespace Components.GoBased
{
    public class RemoteSpawner : SpawnComponent
    {
        private void OnEnable()
        {
            EnterTriggerAction.TriggerDetected += OnTriggerDetected;
        }

        private void OnDisable()
        {
            EnterTriggerAction.TriggerDetected -= OnTriggerDetected;
        }
        
        private void OnTriggerDetected()
        {
            Spawn();
        }
    }
}