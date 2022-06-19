using System;
using UnityEngine;

namespace DefaultNamespace
{
    [RequireComponent(typeof(PirateMover))]
    public class Pirate : MonoBehaviour
    {
        private PirateMover _mover;
        private int _score;

        public event Action GameOver;
        public event Action<int> ScoreChanged; 

        private void Start()
        {
            _mover = GetComponent<PirateMover>();
        }

        public void ResetPlayer()
        {
            _score = 0;
            ScoreChanged?.Invoke(_score);
            _mover.Reset();
        }

        public void Die()
        {
            GameOver?.Invoke();
        }

        public void AddScore()
        {
            _score++;
            ScoreChanged?.Invoke(_score);
        }
    }
}