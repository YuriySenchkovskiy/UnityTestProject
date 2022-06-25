using UnityEngine;

namespace Components.ColliderBased
{
    public class EnterTriggerComponent : BaseDetectionComponent
    {
        protected virtual void OnTriggerEnter2D(Collider2D other)
        {
            base.DetectCollider(other);
        }
    }
}
