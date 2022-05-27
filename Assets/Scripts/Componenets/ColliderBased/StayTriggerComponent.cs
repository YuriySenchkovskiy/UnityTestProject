using Scriptes.Utils;
using UnityEngine;

namespace Scriptes.Components.ColliderBased
{
    public class StayTriggerComponent : BaseDetectionComponent
    {
        private void OnTriggerStay2D(Collider2D other)
        {
            base.OnDetection(other);
        }
    }
}