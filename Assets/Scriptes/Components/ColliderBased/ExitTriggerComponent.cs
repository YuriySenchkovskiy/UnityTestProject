using UnityEngine;

namespace Components.ColliderBased
{
    public class ExitTriggerComponent : BaseDetectionComponent
    {
        private void OnTriggerExit2D(Collider2D other)
        {
            base.DetectCollider(other);
        }
    }
}