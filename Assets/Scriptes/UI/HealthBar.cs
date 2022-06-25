using System.Collections;
using Creatures.Health;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private HealthComponent healthComponent;
        [SerializeField] private float _speed;
        [SerializeField] private Image _bar;

        private Coroutine _currentCoroutine;
        private float _previousHp;
        private int _maxHp;
        
        private void OnEnable()
        { 
            healthComponent.ChangedHealth += OnChangedHealth;
        }
        
        private void Start()
        {
            _maxHp = healthComponent.Health;
            _previousHp = _maxHp;
        }
        
        private void OnDisable()
        { 
            healthComponent.ChangedHealth -= OnChangedHealth;
        }
        
        private void OnChangedHealth(int health)
        {
            StartCurrentCoroutine(ChangeBarValue(health));
        }
        
        private void StartCurrentCoroutine(IEnumerator coroutine)
        {
            if (_currentCoroutine != null)
            {
                StopCoroutine(_currentCoroutine);
            }
            
            _currentCoroutine = StartCoroutine(coroutine);
        }

        private IEnumerator ChangeBarValue(int health)
        {
            while (_previousHp != health)
            {
                _previousHp = Mathf.MoveTowards(_previousHp, health, Time.deltaTime * _speed);
                SetProgress(_previousHp / _maxHp);
                
                yield return null;
            }
        }
        
        private void SetProgress(float progress)
        {
            _bar.fillAmount = progress;
        }
    }
}