using UnityEngine;

namespace Components.GoBased
{
    public class DestroyInvisible : MonoBehaviour
    {
        private void OnBecameInvisible()
        {
            Destroy(gameObject);
        }
    }
}