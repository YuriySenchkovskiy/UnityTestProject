using Scriptes.Utils;
using UnityEngine;

namespace Scriptes.Components.ColliderBased
{
    public abstract class BaseDetectionComponent : MonoBehaviour
    {
        [SerializeField] private LayerMask _layer = ~0;
        [SerializeField] private EnterEventComponent _action; 

        protected void OnDetection(Collision2D other)
        {
            if (!other.gameObject.IsInLayer(_layer))
            {
                return;
            }

            _action?.Invoke(other.gameObject);
        }
        
        protected void OnDetection(Collider2D other)
        {
            if (!other.gameObject.IsInLayer(_layer))
            {
                return;
            }

            _action?.Invoke(other.gameObject);
        }
    }
}