using UnityEngine;

namespace Components.ColliderBased
{
    public class ExitCollisionComponent : BaseDetectionComponent
    {
        private void OnCollisionExit2D(Collision2D other)
        {
            base.DetectCollision(other);
        }
    }
}