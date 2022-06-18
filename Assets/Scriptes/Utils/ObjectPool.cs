using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Utils
{
    public class ObjectPool : MonoBehaviour
    {
        [SerializeField] private GameObject _container;
        [SerializeField] private int _capacity;

        private Camera _camera;

        private List<GameObject> _pool = new List<GameObject>();
        
        public void ResetPool()
        {
            foreach (var item in _pool)
            {
                item.SetActive(false);
            }
        }

        protected void Instantiate(GameObject prefab)
        {
            _camera = Camera.main;

            for (int i = 0; i < _capacity; i++)
            {
                GameObject spawned = Instantiate(prefab, _container.transform);
                spawned.SetActive(false);
                
                _pool.Add(spawned);
            }
        }

        protected bool TryGetObject(out GameObject result)
        {
            result = _pool.FirstOrDefault(p => p.activeSelf == false);

            return result != null;
        }

        protected void DisableObjAbroadScreen()
        {
            Vector3 disablePoint = _camera.ViewportToWorldPoint(new Vector2(-1.5f, 0f));
                
            foreach (var item in _pool)
            {
                if (!item.activeSelf) 
                    continue;
                
                if (item.transform.position.x < disablePoint.x)
                {
                    item.SetActive(false);
                }
            }
        }
    }
}