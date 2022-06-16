using Components;
using UnityEngine;

namespace UI
{
    public class ProgressBar : Bar
    {
        [SerializeField] private Spawner _spawner;
        
        private void OnEnable()
        {
            _spawner.EnemyChanged += OnValueChanged;
            Slider.value = 0;
        }

        private void OnDisable()
        {
            _spawner.EnemyChanged -= OnValueChanged;
        }
    }
}