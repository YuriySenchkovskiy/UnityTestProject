using System;
using UnityEngine;
using UnityEngine.Events;

namespace Components.Health
{
    public class HealthComponent : MonoBehaviour
    {
        [SerializeField] private HealthChangeEvent _changedHp;
        [SerializeField] private UnityEvent _damaged;
        [SerializeField] private UnityEvent _healed;
        [SerializeField] private int _health;

        public int Health => _health;
        private int _startValue;

        public void ModifyHealth(int healthDelta)
        {
            _health += healthDelta;
            
            if (_health <= 0)
            {
                _health = 0;
            }
            else if (_health > _startValue)
            {
                _health = _startValue;
            }

            if (healthDelta < 0)
            {
                _damaged?.Invoke();
            }
            else if (healthDelta > 0)
            {
                _healed?.Invoke();
            }
            
            _changedHp?.Invoke(_health);
        }
        
        private void Start()
        {
            _startValue = _health;
        }

        [Serializable]
        public class HealthChangeEvent : UnityEvent<int> {}
    }
}