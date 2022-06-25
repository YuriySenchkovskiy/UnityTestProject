using UnityEngine;

namespace Components.ColliderBased
{
    public class StayTriggerComponent : BaseDetectionComponent
    {
        private void OnTriggerStay2D(Collider2D other)
        {
            base.DetectCollider(other);
        }
    }
}