using UnityEngine;
using Utils;
using Utils.ObjectPool;

namespace Components.GoBased
{
    public class SpawnComponent : MonoBehaviour
    {
        [SerializeField] protected Transform _target;
        [SerializeField] protected GameObject _prefab; 
        [SerializeField] protected bool _isInvertXScale;
        [SerializeField] protected bool _isInvertYScale;
        [SerializeField] protected bool _isUsePool;

        [ContextMenu("Points")]
        public void Spawn()
        {
            SpawnInstance();
        }

        protected GameObject SpawnInstance()
        {
            var instance = _isUsePool 
                ? Pool.Instance.GetGameObject(_prefab, _target.position) 
                : SpawnUtils.Spawn(_prefab, _target.position);

            var scale = _target.lossyScale;
            scale.x *= _isInvertXScale ? -1 : 1;
            scale.y *= _isInvertYScale ? -1 : 1;
            instance.transform.localScale = scale;
            instance.SetActive(true);
            return instance;
        }
    }
}