using UnityEngine;
using Utils;
using Utils.ObjectPool;

namespace Components.GoBased
{
    public class SpawnComponent : MonoBehaviour
    {
        [SerializeField] protected Transform Target;
        [SerializeField] protected GameObject Prefab; 
        [SerializeField] protected bool IsInvertXScale;
        [SerializeField] protected bool IsInvertYScale;
        [SerializeField] protected bool IsUsePool;

        [ContextMenu("Points")]
        public void Spawn()
        {
            SpawnInstance();
        }

        protected GameObject SpawnInstance()
        {
            var instance = IsUsePool
                ? Pool.Instance.GetGameObject(Prefab, Target.position) 
                : SpawnUtils.Spawn(Prefab, Target.position);

            var scale = Target.lossyScale;
            scale.x *= IsInvertXScale ? -1 : 1;
            scale.y *= IsInvertYScale ? -1 : 1;
            instance.transform.localScale = scale;
            instance.SetActive(true);
            return instance;
        }
    }
}