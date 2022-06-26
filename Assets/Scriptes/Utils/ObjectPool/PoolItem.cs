using System;
using UnityEngine;
using UnityEngine.Events;

namespace Utils.ObjectPool
{
    public class PoolItem : MonoBehaviour
    {
        [SerializeField] private float _releasePoint;
        [SerializeField] private UnityEvent _restart;

        private int _id;
        private Pool _pool;
        private Camera _camera;

        private void Awake()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            Vector3 disablePoint = _camera.ViewportToWorldPoint(new Vector2(_releasePoint, 0f));

            if (gameObject.transform.position.x < disablePoint.x)
            {
                Release();
            }
        }

        public void Restart()
        {
            _restart?.Invoke();
        }
        
        public void Release()
        {
            _pool.Release(_id, this);
        }
        
        public void Retain(int id, Pool pool)
        {
            _id = id;
            _pool = pool;
        }
    }
}