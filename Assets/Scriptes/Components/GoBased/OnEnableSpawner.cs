using UnityEngine;

namespace Components.GoBased
{
    public class OnEnableSpawner : SpawnComponent
    {
        private void OnEnable()
        {
            Spawn();
        }
    }
}