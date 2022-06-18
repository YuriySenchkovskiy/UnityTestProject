using System;
using UnityEngine;
using UnityEngine.Events;

namespace Weapon
{
    [RequireComponent(typeof(Animator))]
    public abstract class Weapon : MonoBehaviour
    {
        [SerializeField] protected Bullet Bullet;
        [SerializeField] protected GameObject Effect;
        [SerializeField] protected Transform ShootPoint;
        [SerializeField] protected Transform EffectPoint;

        [SerializeField] private string _label;
        [SerializeField] private int _price;
        [SerializeField] private Sprite _icon;
        [SerializeField] private bool _isBought = false;
        
        protected static readonly int Fire = Animator.StringToHash("Fire");
        protected Animator AnimatorWeapon;

        private void Start()
        {
            AnimatorWeapon = GetComponent<Animator>();
        }

        public string Label => _label;
        public int Price => _price;
        public Sprite Icon => _icon;
        public bool IsBought => _isBought;

        public abstract void Shoot();

        public void Buy()
        {
            _isBought = true;
        }
    }
}