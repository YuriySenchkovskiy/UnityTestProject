using UnityEngine;
using Utils;

namespace Components.ColliderBased
{
    public abstract class BaseDetectionComponent : MonoBehaviour
    {
        [SerializeField] private LayerMask _layer = ~0;
        [SerializeField] private EnterEventComponent _action; 

        protected void DetectCollision(Collision2D other)
        {
            if (!other.gameObject.GetLayerStatus(_layer))
            {
                return;
            }

            _action?.Invoke(other.gameObject);
        }
        
        protected void DetectCollider(Collider2D other)
        {
            if (!other.gameObject.GetLayerStatus(_layer))
            {
                return;
            }

            _action?.Invoke(other.gameObject);
        }
    }
}