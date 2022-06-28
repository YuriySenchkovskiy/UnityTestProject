using System;
using System.Linq;
using UnityEngine;

namespace Model
{
    [Serializable]
    public struct ItemDef
    {
        [SerializeField] private string _id;
        [SerializeField] private string _label;
        [SerializeField] private string _description;
        [SerializeField] private int _effect;
        [SerializeField] private Sprite _icon;
        [SerializeField] private ItemTag[] _tags;
        [SerializeField] private string _price;

        public string Id => _id;
        public string Label => _label;
        public string Description => _description;
        public int Effect => _effect;
        public Sprite Icon => _icon;
        public string Price => _price;
        public bool IsVoid => string.IsNullOrEmpty(_label);

        public bool HasTag(ItemTag tag)
        {
            return _tags?.Contains(tag) ?? false; 
        }
    }
}