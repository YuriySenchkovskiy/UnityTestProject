using System;
using UnityEngine;
using UnityEngine.Events;

namespace Creatures.Health
{
    public class HealthComponent : MonoBehaviour
    {
        [SerializeField] private int _health;
        [SerializeField] private UnityEvent _damaged;
        [SerializeField] private UnityEvent _healed;
        [SerializeField] private UnityEvent _died;
        
        private int _minHealth;
        private int _maxHealth;
        public UnityAction<int> ChangedHealth;

        public int Health => _health;
        
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
            _maxHealth = _health;
            _minHealth = 0;
        }

        public void SetStartValue()
        {
            _health = _maxHealth;
            ChangedHealth?.Invoke(_health);
        }

        public void ApplyHeal(int healthDelta)
        {
            _health = Mathf.Clamp(_health + healthDelta, _minHealth, _maxHealth);
            ChangedHealth?.Invoke(_health);
        }

        public void ApplyDamage(int damage)
        {
            _health = Mathf.Clamp(_health - damage, _minHealth, _maxHealth);
            
            ChangedHealth?.Invoke(_health);

            if (_health == _minHealth)
            {
                _died?.Invoke();
            }
        }

        [Serializable]
        public class HealthChangeEvent : UnityEvent<int> {}
    }
}