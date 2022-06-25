using UnityEngine;

namespace Components.ColliderBased
{
    public class LineCastCheck : LayerCheck
    {
        [SerializeField] private Transform _target;
        private readonly RaycastHit2D[] _results = new RaycastHit2D[1];

        private void Update()
        {
            IsOnLayer = Physics2D.LinecastNonAlloc(
                transform.position, 
                _target.position, 
                _results, Layer) > 0;
        }

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            UnityEditor.Handles.DrawLine(transform.position, _target.position);
        }
#endif
    }
}