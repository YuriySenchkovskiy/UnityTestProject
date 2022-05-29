using Scriptes.Utils;
using Scriptes.Utils.ObjectPool;
using UnityEngine;
using UnityEngine.Serialization;

namespace Scriptes.Components.GoBased
{
    public class SpawnComponent : MonoBehaviour
    {
        [SerializeField] private Transform _target; 
        [SerializeField] private GameObject _prefab; 
        [SerializeField] private bool _isInvertXScale;
        [SerializeField] private bool _isInvertYScale;
        [SerializeField] private bool _isUsePool;
        
        [ContextMenu("Points")]
        public void Spawn()
        {
            SpawnInstance();
        }

        private GameObject SpawnInstance()
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