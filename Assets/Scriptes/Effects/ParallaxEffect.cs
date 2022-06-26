using UnityEngine;

namespace Effects
{
    public class ParallaxEffect : MonoBehaviour
    {
        [SerializeField] private float _effectValue;
        private Transform _followTarget; 

        private float _startX;
        
        private void Start()
        {
            _followTarget = Camera.main.transform;
            _startX = transform.position.x;
        }
        
        private void LateUpdate()
        {
            var currentPosition = transform.position;
            var deltaX = _followTarget.position.x * _effectValue;
            transform.position = new Vector3(_startX + deltaX, currentPosition.y, currentPosition.z);
        }
    }
}