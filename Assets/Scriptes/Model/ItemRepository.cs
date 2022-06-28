using System.Collections.Generic;
using UnityEngine;

namespace Model
{
    [CreateAssetMenu(fileName = FileName, menuName = "Items", order = 51)]
    public class ItemRepository : ScriptableObject
    {
        [SerializeField] protected ItemDef[] _collection;
        
        private const string FileName = "Definition Items";
        public ItemDef[] Collection => new List<ItemDef>(_collection).ToArray();
        
        public ItemDef Get(string id)
        {
            if (string.IsNullOrEmpty(id))
                return default;
            
            foreach (var itemDef in _collection)
            {
                if (itemDef.Id == id)
                    return itemDef;
            }

            return default;
        }
        
#if UNITY_EDITOR
        public ItemDef[] ItemsForEditor => _collection;
#endif
    }
}