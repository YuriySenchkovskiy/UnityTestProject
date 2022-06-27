using UnityEngine;

namespace Components.GoBased
{
    public class StartTransformSaver : MonoBehaviour
    {
        [SerializeField] private Vector3 _startPosition;

        public void SetStartPosition()
        {
            transform.localPosition = _startPosition;
        }
    }
}