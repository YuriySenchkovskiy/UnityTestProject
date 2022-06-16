using Scriptes.Creatures.Hero;
using UnityEngine;

namespace Scriptes.Creatures.StateMachine
{
    [RequireComponent(typeof(Enemy))]
    public class EnemyStateMachine : MonoBehaviour
    {
        [SerializeField] private State _firstState;
        
        private Player _target;
        private State _currentState;

        public State Current => _currentState;

        private void Start()
        {
            _target = GetComponent<Enemy>().Target;
            Reset(_firstState);
        }

        private void Update()
        {
            if (_currentState == null)
            {
                return;
            }

            var next = _currentState.GetNext();

            if (next != null)
            {
                Transit(next);
            }
        }

        private void Reset(State startState)
        {
            _currentState = startState;

            if (_currentState != null)
            {
                _currentState.Enter(_target);
            }
        }

        private void Transit(State nextState)
        {
            if (_currentState != null)
            {
                _currentState.Exit();
            }

            _currentState = nextState;
            if (_currentState != null)
            {
                _currentState.Enter(_target);
            }
        }
    }
}