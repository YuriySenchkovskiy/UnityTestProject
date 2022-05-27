using Scriptes.Utils;
using UnityEngine;

namespace Scriptes.Components.ColliderBased
{
    public class ExitTriggerComponent : BaseDetectionComponent
    {
        private void OnTriggerExit2D(Collider2D other)
        {
            base.OnDetection(other);
        }
    }
}