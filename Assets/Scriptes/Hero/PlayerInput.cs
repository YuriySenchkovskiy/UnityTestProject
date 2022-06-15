using System;
using UnityEngine;

namespace Scriptes.Creatures.Hero
{
    [RequireComponent(typeof(PlayerMover))]
    public class PlayerInput : MonoBehaviour
    {
        private PlayerMover _playerMover;

        private void Awake()
        {
            _playerMover = GetComponent<PlayerMover>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                _playerMover.TryMoveUp();
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                _playerMover.TryMoveDown();
            }
        }
    }
}