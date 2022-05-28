using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace Scriptes.Creatures.Mobs
{
    public class Bomb : MonoBehaviour
    {
        [SerializeField] private float _waitTimeFrom;
        [SerializeField] private float _waitTimeTo;
        [SerializeField] private UnityEvent _detonate;
        
        private Coroutine _currentCoroutine;
        private WaitForSeconds _waitFor;
        
        private void OnEnable()
        {
            var waitTime = Random.Range(_waitTimeFrom, _waitTimeTo);
            _waitFor = new WaitForSeconds(waitTime);
            _currentCoroutine = StartCoroutine(WaitAndDetonate());
        }

        private IEnumerator WaitAndDetonate()
        {
            yield return _waitFor;
            _detonate?.Invoke();
            _currentCoroutine = null;
        }

        private void OnDisable()
        {
            if(_currentCoroutine != null)
                StopCoroutine(_currentCoroutine);
            _currentCoroutine = null;
        }
    }
}