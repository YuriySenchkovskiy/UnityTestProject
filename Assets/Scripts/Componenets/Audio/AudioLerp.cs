using System.Collections;
using UnityEngine;

namespace Scriptes.Components.Audio
{
    public class AudioLerp : MonoBehaviour
    {
        [SerializeField] private AudioSource _audio;
        [SerializeField] private float _totalTime = 3;
        
        private IEnumerator _current;
        private float _startVolume = 0.0f;
        private float _finalVolume = 0.7f;

        public void TurnOn()
        {
            _audio.volume = _startVolume;
            StopCurrent(TurnOnLerp());
            _audio.Play();
            StartCoroutine(TurnOnLerp());
        }

        public void TurnOff()
        {
            _audio.volume = _finalVolume;
            StopCurrent(TurnOffLerp());
            _audio.Play();
            StartCoroutine(TurnOffLerp());
        }

        private void StopCurrent(IEnumerator coroutine)
        {
            if (_current != null)
            {
                StopCoroutine(_current);
            }

            _current = coroutine;
        }
        
        private IEnumerator TurnOnLerp()
        {
            float currentTime = 0;
            
            while (_audio.volume < _finalVolume)
            {
                currentTime += Time.deltaTime;
                _audio.volume = Mathf.Lerp(_startVolume, _finalVolume, currentTime / _totalTime);
                yield return null;
            }
        }
        
        private IEnumerator TurnOffLerp()
        {
            float currentTime = 0;
            
            while (_audio.volume > _startVolume)
            {
                currentTime += Time.deltaTime;
                _audio.volume = Mathf.Lerp(_finalVolume, _startVolume, currentTime / _totalTime);
                yield return null;
            }
        }
    }
}