using Scriptes.Creatures.Hero;
using TMPro;
using UnityEngine;

namespace DefaultNamespace.UI
{
    public class HealthDisplay : MonoBehaviour
    {
        [SerializeField] private Player _player;
        [SerializeField] private TMP_Text _healthDisplay;

        private void OnEnable()
        {
            _player.HealthChanged += OnHealthChanged;
        }

        private void OnDisable()
        {
            _player.HealthChanged -= OnHealthChanged;
        }

        private void OnHealthChanged(int health)
        {
            _healthDisplay.text = health.ToString();
        }
    }
}