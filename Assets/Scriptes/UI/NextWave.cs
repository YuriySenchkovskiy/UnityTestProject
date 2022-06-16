using System;
using Components;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class NextWave : MonoBehaviour
    {
        [SerializeField] private Spawner _spawner;
        [SerializeField] private Button _nextWaveButton;

        private void OnEnable()
        {
            _spawner.AllEnemySpawned += OnAllEnemySpawned;
            _nextWaveButton.onClick.AddListener(OnNextButtonClick);
        }

        private void OnDisable()
        {
            _spawner.AllEnemySpawned -= OnAllEnemySpawned;
            _nextWaveButton.onClick.RemoveListener(OnNextButtonClick);
        }

        public void OnAllEnemySpawned()
        {
            _nextWaveButton.gameObject.SetActive(true);
        }

        public void OnNextButtonClick()
        {
            _spawner.NextWave();
            _nextWaveButton.gameObject.SetActive(false);
        }
    }
}