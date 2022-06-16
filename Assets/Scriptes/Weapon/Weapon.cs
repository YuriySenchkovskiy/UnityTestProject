using UnityEngine;
using UnityEngine.Events;

namespace Weapon
{
    public abstract class Weapon : MonoBehaviour
    {
        [SerializeField] protected Bullet Bullet;
        [SerializeField] protected UnityEvent Shooted;
        [SerializeField] protected GameObject Effect;
        [SerializeField] protected Transform EfectPoint;
        
        [SerializeField] private string _label;
        [SerializeField] private int _price;
        [SerializeField] private Sprite _icon;
        [SerializeField] private bool _isBought = false;

        public abstract void Shoot(Transform shootPoint);
    }
}