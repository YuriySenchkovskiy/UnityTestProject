using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.PlayerLoop;

namespace Scriptes.Components.ColliderBased
{
    public class LineCastCheck : LayerCheck
    {
        [SerializeField] private Transform _target;
        private readonly RaycastHit2D[] result = new RaycastHit2D[1];

        private void Update()
        {
            _isTouchingLayer = Physics2D.LinecastNonAlloc(
                transform.position, 
                _target.position, 
                result, layer) > 0;
        }

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            UnityEditor.Handles.DrawLine(transform.position, _target.position);
        }
#endif
    }
}