using System;
using System.Collections;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace DefaultNamespace.UI
{
    [RequireComponent(typeof(Image))]
    public class Heart : MonoBehaviour
    {
        [SerializeField] private float _lerpDuration;

        private Image _heart;

        private void Awake()
        {
            _heart = GetComponent<Image>();
            _heart.fillAmount = 1;
        }

        public void ToFill()
        {
            StartCoroutine(Filling(0, 1, _lerpDuration, Fill));
        }

        public void ToEmpty()
        {
            StartCoroutine(Filling(1, 0, _lerpDuration, Destroy));
        }

        private IEnumerator Filling(float statValue, float endValue, float duration, UnityAction<float> onLerpEnded)
        {
            float elapsed = 0;
            float nextValue;

            while (elapsed<duration)
            {
                nextValue = Mathf.Lerp(statValue, endValue, elapsed / duration);
                _heart.fillAmount = nextValue;
                elapsed += Time.deltaTime;
                yield return null;
            }
            
            onLerpEnded?.Invoke(endValue);
        }

        private void Destroy(float value)
        {
            _heart.fillAmount = value;
            Destroy(gameObject);
        }

        private void Fill(float value)
        {
            _heart.fillAmount = value;
        }
    }
}