using System.Collections.Generic;
using UnityEngine;

namespace Model
{
    [CreateAssetMenu(fileName = FileName, menuName = "Items", order = 51)]
    public class ItemRepository : ScriptableObject
    {
        [SerializeField] protected ItemDefinition[] _collection;
        
        private const string FileName = "Definition Items";
        public ItemDefinition[] Collection => new List<ItemDefinition>(_collection).ToArray();
        
        public ItemDefinition Get(string id)
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
        public ItemDefinition[] ItemsForEditor => _collection;
#endif
    }
}