using System.Collections;
using Scriptes.Components.GoBased;
using UnityEngine;

namespace Scriptes.Creatures.Mobs.Patrolling
{
    public class PointPatrol : Patrol
    {
        [SerializeField] private Transform[] _points;
        [SerializeField] private float _treshold = 0.6f;
        [SerializeField] private float _waitTime;
        [SerializeField] private string _miss = "Miss";

        private float _startSpeed;
        private float _multiplier = 1.5f;
        private int _zeroValue = 0;
        private int _destinationPointIndex;
        
        private int _nextPoint = 1;
        private Creatures _creature;
        private SpawnListComponent _particles;
        
        public override IEnumerator DoPatrol()
        {
            while (enabled)
            {
                if (isOnPoint())
                {
                    _creature.Speed = _zeroValue;
                    yield return new WaitForSeconds(_waitTime);
                    _particles.Spawn(_miss);
                    
                    _destinationPointIndex = (int) Mathf.Repeat(_destinationPointIndex + _nextPoint, _points.Length);
                    _creature.Speed = _startSpeed * _multiplier;
                }
                
                var direction = _points[_destinationPointIndex].position - transform.position;
                direction.y = _zeroValue;
                _creature.SetDirection(direction.normalized);

                yield return null;
            }
        }

        private void Awake()
        {
            _creature = GetComponent<Creatures>();
            _particles = GetComponent<SpawnListComponent>();
            _startSpeed = _creature.Speed;
        }
        
        private bool isOnPoint()
        {
            return (_points[_destinationPointIndex].position - transform.position).magnitude < _treshold;
        }
    }
}