using System;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
#if UNITY_EDITOR
#endif

namespace Components.ColliderBased
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