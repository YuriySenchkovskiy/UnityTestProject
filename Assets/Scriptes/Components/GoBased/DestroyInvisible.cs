using System;
using UnityEngine;

namespace Scriptes.Components.GoBased
{
    public class DestroyInvisible : MonoBehaviour
    {
        private void OnBecameInvisible()
        {
            Destroy(gameObject);
        }
    }
}