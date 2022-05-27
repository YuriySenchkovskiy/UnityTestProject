using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Scriptes.Animation
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class SpriteAnimator : MonoBehaviour
    {
        [Header("Settings")] 
        [SerializeField] [Range(1, 30)] private int _frameRate = 10;
        [SerializeField] private AnimationClip[] _clips;
        [SerializeField] private UnityEvent<string> _endAnimation;
        
        private SpriteRenderer _renderer;
        private float _secondPerFrame;
        private float _nextFrameTime; 
        private int _currentFrame;
        
        private bool _isPlaying = true;
        private int _currentClip;
        private int _nextClip = 1;

        public void SetClip(string name)
        {
            for (var i = 0; i < _clips.Length; i++)
            {
                if (_clips[i].Name == name)
                {
                    _currentClip = i;
                    StartAnimation();
                    return;
                }
            }

            enabled = _isPlaying = false;
        }
        
        private void OnEnable() 
        {
            _nextFrameTime = Time.time;
        }

        private void Start()
        {
            _renderer = GetComponent<SpriteRenderer>();
            _secondPerFrame = 1f / _frameRate;
            StartAnimation();
        }

        private void OnBecameVisible() 
        {
            enabled = _isPlaying;
        }

        private void OnBecameInvisible()
        {
            enabled = false;
        }

        private void Update()
        {
            if (_nextFrameTime > Time.time)
            {
                return;
            }
            
            var clip = _clips[_currentClip]; 
            if (_currentFrame >= clip.Sprites.Length)
            {
                if (clip.Loop)
                {
                    _currentFrame = 0;
                }
                else
                {
                    enabled = _isPlaying = clip.AllowNextClip;
                    clip.OnComplete?.Invoke();
                    _endAnimation?.Invoke(clip.Name);

                    if (clip.AllowNextClip)
                    {
                        _currentFrame = 0;
                        _currentClip = (int) Mathf.Repeat(_currentClip + _nextClip, _clips.Length);
                    }
                }

                return;
            }

            _renderer.sprite = clip.Sprites[_currentFrame];
            _nextFrameTime += _secondPerFrame;
            _currentFrame++;
        }

        private void StartAnimation()
        {
            _nextFrameTime = Time.time;
            enabled = _isPlaying = true;
            if (!_clips[_currentClip].Random)
            {
                _currentFrame = 0; 
            }
            else
            {
                _currentFrame = Random.Range(0, _clips[_currentClip].Sprites.Length);
            }
        }

        [Serializable]
        private class AnimationClip
        {
            [SerializeField] private string _name;
            [SerializeField] private bool _loop;
            [SerializeField] private bool _random;
            [SerializeField] private bool _allowNextClip;
            
            [SerializeField] private Sprite[] _sprites;
            [SerializeField] private UnityEvent _onComplete;
            
            public string Name => _name;
            public Sprite[] Sprites => _sprites;
            public bool Loop => _loop;
            public bool Random => _random;
            public bool AllowNextClip => _allowNextClip;
            public UnityEvent OnComplete => _onComplete;
        }
    }
}