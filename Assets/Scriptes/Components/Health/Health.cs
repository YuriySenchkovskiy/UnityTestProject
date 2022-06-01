using System;
using UnityEngine;
using UnityEngine.Events;

namespace Components.Health
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private int _healthValue;
        [SerializeField] private HealthChangeEvent _changedHp;
        [SerializeField] private UnityEvent _damaged;
        [SerializeField] private UnityEvent _healed;

        public int HealthValue => _healthValue;
        private int _startHealthValue;

        public void ApplyHeal(int healthDelta)
        {
            _healthValue += healthDelta;

            if (_healthValue > _startHealthValue)
            {
                _healthValue = _startHealthValue;
            }

            _healed?.Invoke();
            _changedHp?.Invoke(_healthValue);
        }

        public void ApplyDamage(int damage)
        {
            _healthValue -= damage;
            
            if (_healthValue <= 0)
            {
                _healthValue = 0;
            }
            
            _damaged?.Invoke();
            _changedHp?.Invoke(_healthValue);
        }
        
        private void Start()
        {
            _startHealthValue = _healthValue;
        }

        [Serializable]
        public class HealthChangeEvent : UnityEvent<int> {}
    }
}