using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

namespace Components.GoBased
{
    public class SpawnListComponent : MonoBehaviour
    {
        [SerializeField] private SpawnData[] _spawners;

        public void SpawnAll()
        {
            foreach (var spawnData in _spawners)
            {
                spawnData.Component.Spawn();
            }
        }
        
        public void Spawn(string id) 
        {
            var spawner = _spawners.FirstOrDefault(element => element.Id == id);
            spawner?.Component.Spawn();
        }

        [Serializable]
        public class SpawnData
        {
            [FormerlySerializedAs("id")] public string Id;
            [FormerlySerializedAs("component")] public SpawnComponent Component;
        }
    }
}
