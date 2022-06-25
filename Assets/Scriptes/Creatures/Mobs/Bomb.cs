using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace Creatures.Mobs
{
    public class Bomb : MonoBehaviour
    {
        [SerializeField] private float _waitTimeFrom;
        [SerializeField] private float _waitTimeTo;
        [SerializeField] private UnityEvent _detonated;

        private void OnEnable()
        {
            var waitTime = Random.Range(_waitTimeFrom, _waitTimeTo);
            Invoke(nameof(Detonate), waitTime);
        }

        private void Detonate()
        {
            _detonated?.Invoke();
        }
    }
}