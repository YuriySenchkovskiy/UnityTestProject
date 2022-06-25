using UnityEngine;

namespace Utils
{
    public static class GameObjectExtensions
    {
        public static bool GetLayerStatus(this GameObject go, LayerMask layer)
        {
            return layer == (layer | 1 << go.layer);
        }
    }
}