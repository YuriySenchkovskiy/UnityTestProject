using Animation;
using UnityEngine;

namespace Weapon
{
    public abstract class Weapon : MonoBehaviour
    {
        [SerializeField] protected Bullet Bullet;
        [SerializeField] private string _label;
        [SerializeField] private int _price;
        [SerializeField] private Sprite _icon;

        [SerializeField] private SpriteAnimator _spriteAnimator;
        [SerializeField] private bool _isBought = false;
        
        public SpriteAnimator SpriteAnimator => _spriteAnimator;

        public abstract void Shoot(Transform shootPoint);
    }
}