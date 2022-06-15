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
        
        private int _minHealth;
        private int _maxHealth;

        public int HealthValue => _healthValue;
        
        public event UnityAction Damaged
        {
            add => _damaged?.AddListener(value);
            remove => _damaged?.RemoveListener(value);
        }
        
        public event UnityAction Healed
        {
            add => _healed?.AddListener(value);
            remove => _healed?.RemoveListener(value);
        }
        
        private void Start()
        {
            _maxHealth = _healthValue;
            _minHealth = 0;
        }
        
        public void ApplyHeal(int healthDelta)
        {
            _healthValue = Mathf.Clamp(_healthValue + healthDelta, _minHealth, _maxHealth);

            _healed?.Invoke();
            _changedHp?.Invoke(_healthValue);
        }

        public void ApplyDamage(int damage)
        {
            _healthValue = Mathf.Clamp(_healthValue - damage, _minHealth, _maxHealth);
            
            _damaged?.Invoke();
            _changedHp?.Invoke(_healthValue);
        }

        [Serializable]
        public class HealthChangeEvent : UnityEvent<int> {}
    }
}