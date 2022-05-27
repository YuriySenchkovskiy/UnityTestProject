using Scriptes.Utils;
using UnityEngine;

namespace Scriptes.Components.ColliderBased
{
    public class ExitCollisionComponent : BaseDetectionComponent
    {
        private void OnCollisionExit2D(Collision2D other)
        {
            base.DetectCollision(other);
        }
    }
}