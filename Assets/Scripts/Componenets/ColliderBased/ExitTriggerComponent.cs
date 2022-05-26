using Scriptes.Utils;
using UnityEngine;

namespace Scriptes.Components.ColliderBased
{
    public class ExitTriggerComponent : MonoBehaviour
    {
        [SerializeField] private string _tag; 
        [SerializeField] private LayerMask _layer = ~0; 
        [SerializeField] private EnterEventComponent _action; 

        private void OnTriggerExit2D(Collider2D other)
        {
            if (!other.gameObject.IsInLayer(_layer)) return;
            if (!string.IsNullOrEmpty(_tag) && !other.gameObject.CompareTag(_tag)) return; 
            
            _action?.Invoke(other.gameObject); 
        }
    }
}