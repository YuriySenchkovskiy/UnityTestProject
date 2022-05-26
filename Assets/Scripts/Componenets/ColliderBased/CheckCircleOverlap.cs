using System;
using System.Collections.Generic;
using System.Linq;

#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine;
using UnityEngine.Events;

namespace Scriptes.Components.ColliderBased
{
    public class CheckCircleOverlap : MonoBehaviour
    {
        [SerializeField] private float _radius = 1f; 
        [SerializeField] private LayerMask _mask;
        [SerializeField] private OnOverlapEvent _onOverlap; 
        [SerializeField] private string[] _tags; 
        private Collider2D[] _interactionResult = new Collider2D[10];
        
        public void Check()
        {
            var size = Physics2D.OverlapCircleNonAlloc(
                transform.position,
                _radius,
                _interactionResult,
                _mask); 
            
            var overlaps = new List<GameObject>(); 
            for (var i = 0; i < size; i++)
            {
                var overlapResult = _interactionResult[i];
                var isInTag = _tags.Any(tag => overlapResult.CompareTag(tag));
                if(isInTag)
                {
                    _onOverlap?.Invoke(_interactionResult[i].gameObject); 
                }
            }
        }

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Handles.color = Utils.HandlesUtils.TransparentRed;
            Handles.DrawSolidDisc(transform.position, Vector3.forward, _radius);
        }
#endif
        
        [Serializable]
        public class OnOverlapEvent : UnityEvent<GameObject> {}
    }
}