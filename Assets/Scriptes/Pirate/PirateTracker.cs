using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class PirateTracker : MonoBehaviour
    {
        [SerializeField] private Pirate _pirate;
        [SerializeField] private float _offset;

        private void LateUpdate()
        {
            transform.position = new Vector3(_pirate.transform.position.x + _offset, transform.position.y);
        }
    }
}