using System;
using UnityEngine;

namespace Components.GoBased
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField, Range(0,3)] private float _smooth;
        [SerializeField, Range(0,5)] private float _yOffset;
        [SerializeField, Range(0f,0.1f)] private float _xSpeed;
        [SerializeField] private float _xCameraPosition;

        private float _oldTargetXPosition;

        private void Start()
        {
            _oldTargetXPosition = _target.position.x;
        }

        private void LateUpdate()
        {
            _xCameraPosition = _target.position.x == _oldTargetXPosition 
                               && _target.position.x >= _xCameraPosition ? 
                                    _target.position.x : _xCameraPosition; 
            _xCameraPosition += _xSpeed;
            
            var xValue = _target.position.x > _xCameraPosition ? 
                                    _target.position.x : _xCameraPosition;
            
            var destination = new Vector3(xValue, _target.position.y + _yOffset, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, destination, Time.deltaTime * _smooth);
            _oldTargetXPosition = _target.position.x;
        }
    }
}