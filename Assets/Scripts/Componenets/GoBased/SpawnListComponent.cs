using System;
using System.Linq;
using UnityEngine; 

namespace Scriptes.Components.GoBased
{
    public class SpawnListComponent : MonoBehaviour
    {
        [SerializeField] private SpawnData[] _spawners;

        public void SpawnAll()
        {
            foreach (var spawnData in _spawners)
            {
                spawnData.component.Spawn();
            }
        }
        
        public void Spawn(string id) 
        {
            var spawner = _spawners.FirstOrDefault(element => element.id == id);
            spawner?.component.Spawn();
        }

        [Serializable]
        public class SpawnData
        {
            public string id;
            public SpawnComponent component;
        }
    }
}
