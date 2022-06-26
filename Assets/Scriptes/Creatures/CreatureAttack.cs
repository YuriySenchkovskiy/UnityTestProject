using Components.Audio;
using Components.ColliderBased;
using Components.GoBased;
using UnityEngine;

namespace Creatures
{
    [RequireComponent(typeof(Animator), typeof(PlaySoundsComponent), typeof(SpawnListComponent))]
    [RequireComponent(typeof(CreatureMover))]
    public class CreatureAttack : MonoBehaviour
    {
        [Space] [Header("Attack")]
        [SerializeField] private CheckCircleOverlap _attackRange;
        [SerializeField] private string _melee = "Melee";

        private CreatureMover _creatureMover;
        private SpawnListComponent _particles;
        private Animator _animator;
        private static readonly int AttackKey = Animator.StringToHash("attack");

        private PlaySoundsComponent _sounds;
        
        private void Awake()
        {
            _creatureMover = GetComponent<CreatureMover>();
            _animator = GetComponent<Animator>();
            _particles = GetComponent<SpawnListComponent>();
            _sounds = GetComponent<PlaySoundsComponent>();
        }

        public void Attack()
        {
            if (!_creatureMover.IsGrounded)
            {
                return;
            }

            _animator.SetTrigger(AttackKey);
            _particles.Spawn(_melee);
            _sounds.Play(_melee);
        }
        
        public void AttackInAnimation()
        {
            _attackRange.Check(); 
        }
    }
}