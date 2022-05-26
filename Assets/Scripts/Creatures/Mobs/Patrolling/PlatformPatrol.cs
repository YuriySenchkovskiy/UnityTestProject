using System;
using System.Collections;
using Scriptes.Components.ColliderBased;
using UnityEngine;
using UnityEngine.Events;

namespace Scriptes.Creatures.Mobs.Patrolling
{
    public class PlatformPatrol : Patrol
    {
        [SerializeField] private LayerCheck _isGround;
        [SerializeField] private LayerCheck _obstacleCheck;
        [SerializeField] private OnChangeDirection _onChangeDirection;
        [SerializeField] private int _direction;
        
        public override IEnumerator DoPatrol()
        {
            while (enabled)
            {
                if (_isGround.isTouchingLayer && !_obstacleCheck.isTouchingLayer)
                {
                    _onChangeDirection?.Invoke(new Vector2(_direction, 0));
                }
                else
                {
                    _onChangeDirection?.Invoke(Vector2.zero);
                    _direction = -_direction; 
                    _onChangeDirection?.Invoke(new Vector2(_direction, 0));
                }
                
                yield return null;
            }
        }
    }

    [Serializable]
    public class OnChangeDirection : UnityEvent<Vector2>
    {
    }
}