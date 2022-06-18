using System;
using Pipes;
using UnityEngine;

namespace DefaultNamespace
{
    [RequireComponent(typeof(Pirate))]
    public class PirateCollisionHandler : MonoBehaviour
    {
        private Pirate _pirate;

        private void Start()
        {
            _pirate = GetComponent<Pirate>();
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.TryGetComponent(out ScoreZone scoreZone))
            {
                _pirate.AddScore();
            }
            else
            {
                _pirate.Die();
            }
        }
    }
}