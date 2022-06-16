using UnityEngine;

namespace Scriptes.Creatures.Hero
{
    public class PlayerMover : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _stepSize; // будем делать пошаговое движение
        [SerializeField] private float _maxHeight;
        [SerializeField] private float _minHeight;

        private Vector3 _targetPosition;

        private void Start()
        {
            _targetPosition = transform.position;
        }

        private void Update()
        {
            if (transform.position != _targetPosition)
            {
                // time.deltaTime - тут чтобы сгладить разницу fps у разных пользователей
                transform.position = Vector3.MoveTowards(transform.position, _targetPosition, _moveSpeed * Time.deltaTime);
            }
        }

        public void TryMoveUp()
        {
            if (_targetPosition.y <= _maxHeight)
            {
                SetNextPosition(_stepSize);
            }
        }
        
        public void TryMoveDown()
        {
            if (_targetPosition.y >= _minHeight)
            {
                SetNextPosition(-_stepSize);
            }
        }

        private void SetNextPosition(float stepSize)
        {
            _targetPosition = new Vector2(_targetPosition.x, _targetPosition.y + stepSize);
        }
    }
}