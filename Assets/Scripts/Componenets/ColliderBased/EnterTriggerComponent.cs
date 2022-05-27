using Scriptes.Utils;
using UnityEngine;

namespace Scriptes.Components.ColliderBased
{
    public class EnterTriggerComponent : BaseDetectionComponent
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            base.OnDetection(other);
        }
    }
}
