using System;
using UnityEngine;

namespace DefaultNamespace
{
    [RequireComponent(typeof(PirateMover))]
    public class Pirate : MonoBehaviour
    {
        private PirateMover _mover;
        private int _score;

        private void Start()
        {
            _mover = GetComponent<PirateMover>();
        }

        public void ResetPlayer()
        {
            _score = 0;
            _mover.Reset();
        }

        public void Die()
        {
            Time.timeScale = 0;
        }

        public void AddScore()
        {
            ++_score;
        }
    }
}