using System;
using UnityEngine;

namespace Utils.ObjectPool
{
    public class Releaser : MonoBehaviour
    {
        [SerializeField] private float _length;

        private PoolItem _poolItem;
        private Camera _camera;
        private float _startPosition;
        
        private void Awake()
        {
            _poolItem = GetComponent<PoolItem>();
            _camera = Camera.main;
        }

        private void OnEnable()
        {
            _startPosition = _camera.gameObject.transform.position.x;
        }

        private void Update()
        {
            if (_camera.gameObject.transform.position.x - _startPosition > _length)
            {
                _poolItem.Release();
            }
        }
    }
}