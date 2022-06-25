using System.Collections;
using Components.GoBased;
using UnityEngine;

namespace Creatures.Mobs.Patrolling
{
    [RequireComponent(typeof(Creatures), typeof(SpawnListComponent))]
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
        private WaitForSeconds _waitTimeSeconds;

        private bool _isOnPoint => (_points[_destinationPointIndex].position - transform.position).magnitude < _treshold;

        private void Awake()
        {
            _creature = GetComponent<Creatures>();
            _particles = GetComponent<SpawnListComponent>();
            _startSpeed = _creature.Speed;
        }

        private void Start()
        {
            _waitTimeSeconds = new WaitForSeconds(_waitTime);
        }
        
        public override IEnumerator DoPatrol()
        {
            while (enabled)
            {
                if (_isOnPoint)
                {
                    _creature.Speed = _zeroValue;
                    _particles.Spawn(_miss);
                    yield return _waitTimeSeconds;

                    _destinationPointIndex = (int) Mathf.Repeat(_destinationPointIndex + _nextPoint, _points.Length);
                    _creature.Speed = _startSpeed * _multiplier;
                }
                
                var direction = _points[_destinationPointIndex].position - transform.position;
                direction.y = _zeroValue;
                _creature.SetDirection(direction.normalized);

                yield return null;
            }
        }
    }
}