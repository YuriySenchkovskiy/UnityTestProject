using System.Collections;
using UnityEngine;

namespace Scriptes.Components.Audio
{
    public class AudioParabola : MonoBehaviour
    {
        [SerializeField] private AudioSource _audio;
        [SerializeField] private float _totalTime = 4.5f;

        private Coroutine _currentCoroutine;
        private float _startVolume = 0.0f;
        private float _targetVolume = 0.7f;

        public void ChangeSoundVolume()
        {
            _audio.Play();
            _audio.volume = _startVolume;
            StartCurrentCoroutine(GetSoundVolume());
        }

        private void StartCurrentCoroutine(IEnumerator coroutine)
        {
            if (_currentCoroutine != null)
            {
                StopCoroutine(_currentCoroutine);
            }
            
            _currentCoroutine = StartCoroutine(coroutine);
        }

        private IEnumerator GetSoundVolume()
        {
            float currentTime = 0;

            while (_audio.volume != _targetVolume)
            {
                currentTime += Time.deltaTime;
                _audio.volume = Mathf.MoveTowards(_startVolume, _targetVolume, currentTime / _totalTime);
                yield return null;
            }
            
            (_targetVolume, _startVolume) = (_startVolume, _targetVolume);
        }
    }
}