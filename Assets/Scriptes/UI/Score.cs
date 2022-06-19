using System;
using TMPro;
using UnityEngine;

namespace DefaultNamespace.UI
{
    public class Score : MonoBehaviour
    {
        [SerializeField] private Pirate _pirate;
        [SerializeField] private TMP_Text _score;

        private void OnEnable()
        {
            _pirate.ScoreChanged += OnScoreChanged;
        }

        private void OnDisable()
        {
            _pirate.ScoreChanged -= OnScoreChanged;
        }

        private void OnScoreChanged(int score)
        {
            _score.text = score.ToString();
        }
    }
}