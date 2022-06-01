using System.Collections;
using System.Collections.Generic;
using Components.Health;
using UnityEngine;

namespace UI
{
    public class LifeBarWidget : MonoBehaviour
    {
        [SerializeField] private HealthBarWidget _healthBar;
        [SerializeField] private HealthComponent _hp;
        [SerializeField] private float _step;

        private Coroutine _currentCoroutine;
        private float _previousHp;
        private int _maxHp;
        
        public void ChangeBarValue(int hp)
        {
            StartCurrentCoroutine(SetLifeBarValue(hp));
        }
        
        private void StartCurrentCoroutine(IEnumerator coroutine)
        {
            if (_currentCoroutine != null)
            {
                StopCoroutine(_currentCoroutine);
            }
            
            _currentCoroutine = StartCoroutine(coroutine);
        }

        private IEnumerator SetLifeBarValue(int hp)
        {
            var currentTime = 0f;
            
            while (_previousHp != hp)
            {
                currentTime += Time.deltaTime;
                _previousHp = Mathf.MoveTowards(_previousHp, hp, currentTime / _step);
                _healthBar.SetProgress(_previousHp/_maxHp);
                
                yield return null;
            }
        }

        private void Start()
        {
            _maxHp = _hp.Health;
            _previousHp = _maxHp;
        }
    }
}