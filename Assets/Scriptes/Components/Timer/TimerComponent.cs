using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Components.Timer
{
    public class TimerComponent : MonoBehaviour
    {
        [SerializeField] private bool _isCountFromStart;
        [SerializeField] private int _timerNumber;
        [SerializeField] private TimerData[] _timers;

        private void Start()
        {
            if (_isCountFromStart)
            {
                SetTimer(_timerNumber);
            }
        }
        
        private void SetTimer(int index)
        {
            var timer = _timers[index];
            var waitTime = new WaitForSeconds(timer.Delay);
            StartCoroutine(StartTimer(timer, waitTime));
        }

        private IEnumerator StartTimer(TimerData timer, WaitForSeconds waitTime)
        {
            yield return waitTime; 
            timer.EventAtTheEnd?.Invoke();
        }
        
        [Serializable]
        public class TimerData
        {
            public float Delay;
            public UnityEvent EventAtTheEnd;
        }
    }
}