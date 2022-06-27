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
        [SerializeField] protected bool IsUseRandomXPoint;
        [SerializeField] protected float XPointStart;
        [SerializeField] protected float XPointEnd;

        [ContextMenu("Points")]
        public void Spawn()
        {
            SpawnInstance();
        }

        private GameObject SpawnInstance()
        {
            if (IsUseRandomXPoint)
            {
                var _xAdded = Random.Range(XPointStart, XPointEnd);
                var position = new Vector3(Target.position.x + _xAdded, Target.position.y);
                Target.position = position;
            }

            var instance = IsUsePool 
                ?  Pool.Instance.GetGameObject(Prefab, Target.position) 
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