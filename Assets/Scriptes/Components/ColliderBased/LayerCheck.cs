using UnityEngine;

namespace Components.ColliderBased
{
    [RequireComponent(typeof(Collider2D))]
    public class LayerCheck : MonoBehaviour
    {
        [SerializeField] protected LayerMask Layer; 
        [SerializeField] protected bool IsOnLayer;
        private Collider2D _collider2D;
        
        public bool IsTouchingLayer => IsOnLayer;
        
        private void Awake()
        {
            _collider2D = GetComponent<Collider2D>();
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            IsOnLayer = _collider2D.IsTouchingLayers(Layer);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            IsOnLayer = false;
        }
    }
}
