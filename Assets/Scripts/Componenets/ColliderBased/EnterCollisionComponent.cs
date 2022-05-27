using Scriptes.Utils;
using UnityEngine;

namespace Scriptes.Components.ColliderBased
{
    public class EnterCollisionComponent : BaseDetectionComponent
    {
        private void OnCollisionEnter2D(Collision2D other)
        {
            base.OnDetection(other);
        }
    }
}
