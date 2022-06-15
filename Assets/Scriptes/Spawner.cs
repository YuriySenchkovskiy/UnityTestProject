using UnityEngine;
using Random = UnityEngine.Random;

namespace DefaultNamespace
{
    public class Spawner : ObjectPool
    {
        [SerializeField] private GameObject[] _prefabs;
        [SerializeField] private float _cooldown;
        [SerializeField] private Transform[] _spawnPoints;

        private float _elapsed = 0;

        private void Start()
        {
            Initialize(_prefabs);
        }

        private void Update()
        {
            _elapsed += Time.deltaTime;

            if (_elapsed >= _cooldown)
            {
                if (TryGetObject(out GameObject enemy))
                {
                    _elapsed = 0;
                    int spawnPointNumber = Random.Range(0, _spawnPoints.Length);
                    SetEnemy(enemy, _spawnPoints[spawnPointNumber].position);
                }
            }
        }

        private void SetEnemy(GameObject enemy, Vector3 spawnPoint)
        {
            enemy.SetActive(true);
            enemy.transform.position = spawnPoint;
        }
    }
}