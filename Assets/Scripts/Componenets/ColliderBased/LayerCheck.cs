using UnityEngine;

namespace Scriptes.Components.ColliderBased
{
    [RequireComponent(typeof(Collider2D))]
    public class LayerCheck : MonoBehaviour
    {
        [SerializeField] protected LayerMask layer; 
        [SerializeField] protected bool _isTouchingLayer;
        private Collider2D _collider2D;
        
        public bool isTouchingLayer => _isTouchingLayer;
        
        private void Awake()
        {
            _collider2D = GetComponent<Collider2D>();
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            _isTouchingLayer = _collider2D.IsTouchingLayers(layer);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            _isTouchingLayer = false;
        }
    }
}
