using System;
using System.Collections.Generic;
using Scriptes.Creatures;
using Scriptes.Creatures.Hero;
using UnityEngine;

namespace Components
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private List<Wave> _waves;
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private Player _player;

        private Wave _currentWave;
        private int _currentNumber = 0;
        private float _timeAfterLastSpawn;
        private int _spawned;

        public event Action AllEnemySpawned;
        public event Action<int, int> EnemyChanged;

        private void Start()
        {
            SetWave(_currentNumber);
        }

        private void Update()
        {
            if (_currentWave == null)
            {
                return;
            }

            _timeAfterLastSpawn += Time.deltaTime;

            if (_timeAfterLastSpawn > _currentWave.Delay)
            {
                InstantiateEnemy();
                _spawned++;
                _timeAfterLastSpawn = 0;
                EnemyChanged?.Invoke(_spawned, _currentWave.Count);
            }

            if (_currentWave.Count <= _spawned)
            {
                if (_waves.Count > _currentNumber + 1)
                {
                    AllEnemySpawned?.Invoke();
                }
                
                _currentWave = null;
            }
        }

        public void NextWave()
        {
            SetWave(++_currentNumber);
            _spawned = 0;
        }

        private void InstantiateEnemy()
        {
            Enemy enemy = Instantiate(_currentWave.Template, _spawnPoint.position, _spawnPoint.rotation)
                .GetComponent<Enemy>();
            enemy.Init(_player);
            enemy.Dying += OnEnemyDying;
        }

        private void SetWave(int index)
        {
            _currentWave = _waves[index];
            EnemyChanged?.Invoke(0, _currentWave.Count);
        }

        private void OnEnemyDying(Enemy enemy)
        {
            enemy.Dying -= OnEnemyDying;
            _player.AddMoney(enemy.Reward);
        }
    }

    [Serializable]
    public class Wave
    {
        public GameObject Template;
        public float Delay;
        public int Count;
    }
}