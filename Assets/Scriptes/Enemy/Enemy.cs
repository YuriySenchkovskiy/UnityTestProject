using Scriptes.Creatures.Hero;
using UnityEngine;

namespace Enemy
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private int _damage;

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.TryGetComponent(out Player player))
            {
                player.ApplyDamage(_damage);
            }
            
            Die();
        }

        private void Die()
        {
            gameObject.SetActive(false);
        }
    }
}