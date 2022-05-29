using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace Components
{
    public class ProbabilityDropComponent : MonoBehaviour
    {
        [SerializeField] private int _count;
        [SerializeField] private DropData[] _items;
        [SerializeField] private DropEvent _dropCalculated;
        [SerializeField] private bool _isSpawnOnEnable;
        
        private void OnEnable()
        {
            if (_isSpawnOnEnable)
            {
                CalculateDrop();
            }
        }

        [ContextMenu("CalculateDrop")] 
        public void CalculateDrop()
        {
            var itemsToDrop = new GameObject[_count];
            var itemCount = 0;
            var total = _items.Sum(dropData => dropData.Probability);
            var sortedDrop = _items.OrderBy(dropData => dropData.Probability);
            
            while (itemCount < _count)
            {
                var random = Random.value * total;
                var current = 0f;
                
                foreach (var dropData in sortedDrop)
                {
                    current += dropData.Probability;
                    if (current >= random) 
                    {
                        itemsToDrop[itemCount] = dropData.Item;
                        itemCount++;
                        break;
                    }
                }
            }
            
            _dropCalculated?.Invoke(itemsToDrop);
        }

        [Serializable]
        public class DropData
        {
            public GameObject Item;
            [Range(0f, 100f)] public float Probability;
        }

        [Serializable]
        public class DropEvent : UnityEvent<GameObject[]> {}
    }
}