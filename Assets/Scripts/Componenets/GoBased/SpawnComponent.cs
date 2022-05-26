using Scriptes.Utils;
using Scriptes.Utils.ObjectPool;
using UnityEngine;

namespace Scriptes.Components.GoBased
{
    public class SpawnComponent : MonoBehaviour
    {
        [SerializeField] private Transform _target; 
        [SerializeField] private GameObject _prefab; 
        [SerializeField] private bool _invertXScale;
        [SerializeField] private bool _invertYScale;
        [SerializeField] private bool _usePool;
        
        [ContextMenu("Points")]
        public void Spawn()
        {
            SpawnInstance();
        }

        public GameObject SpawnInstance()
        {
            var instance = _usePool 
                ? Pool.Instance.Get(_prefab, _target.position) 
                : SpawnUtils.Spawn(_prefab, _target.position);

            var scale = _target.lossyScale;
            scale.x *= _invertXScale ? -1 : 1;
            scale.y *= _invertYScale ? -1 : 1;
            instance.transform.localScale = scale;
            instance.SetActive(true);
            return instance;
        }
    }
}