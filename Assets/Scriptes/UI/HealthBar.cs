using System.Collections;
using Components.Health;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Health _hp;
        [SerializeField] private float _step;
        [SerializeField] private HealthBarUISetter _healthBar;

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
                _healthBar.SetProgress(_previousHp / _maxHp);
                
                yield return null;
            }
        }

        private void Start()
        {
            _maxHp = _hp.HealthValue;
            _previousHp = _maxHp;
        }
    }
}