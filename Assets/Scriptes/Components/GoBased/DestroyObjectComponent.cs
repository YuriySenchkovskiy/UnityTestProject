using UnityEngine;

namespace Scriptes.Components.GoBased
{
    public class DestroyObjectComponent : MonoBehaviour
    {
        [SerializeField] private GameObject _objectToDestroy;
        [SerializeField] private float _waitTime = 0;

        public void DestroyObject()
        {
            Destroy(_objectToDestroy, _waitTime); 
        }
    }
}