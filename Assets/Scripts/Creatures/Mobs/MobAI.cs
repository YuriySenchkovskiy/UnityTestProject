using System.Collections;
using Scriptes.Components.ColliderBased;
using Scriptes.Components.GoBased;
using Scriptes.Creatures.Mobs.Patrolling;
using UnityEngine;

namespace Scriptes.Creatures.Mobs
{
    public class MobAI : MonoBehaviour
    {
        [SerializeField] private LayerCheck _vision;
        [SerializeField] protected LayerCheck _canAttack;
        [SerializeField] protected Creature _creature;
        
        [SerializeField] protected float _alarmDelay = 0.5f; 
        [SerializeField] protected float _attackCooldawn = 1f;
        [SerializeField] private float _missHeroCooldown = 0.5f;

        [SerializeField] protected float _horizontalTrashold = 0.2f; 

        private IEnumerator _current; 
        protected GameObject _target; 
        protected SpawnListComponent _particles;
        
        protected Animator _animator; 
        protected bool _isDead; 
        protected Patrol _patrol;
        
        private static readonly int IsDeadKey = Animator.StringToHash("isDeadKey");
        
        private void Awake()
        {
            _particles = GetComponent<SpawnListComponent>();
            _animator = GetComponent<Animator>();
            _patrol = GetComponent<Patrol>();
        }

        private void Start()
        {
            StartState(_patrol.DoPatrol());
        }

        protected void StartState(IEnumerator coroutine)
        {
            _creature.SetDirection(Vector2.zero); 
            
            if (_current != null)
            {
                StopCoroutine(_current);
            }
            
            _current = coroutine;
            StartCoroutine(coroutine);
        }

        protected virtual IEnumerator AgroToHero()
        {
            LookAtHero();
            _particles.Spawn("Exclamation"); 
            yield return new WaitForSeconds(_alarmDelay); 
            StartState(GoToHero());
        }

        protected void LookAtHero()
        {
            _creature.SetDirection(Vector2.zero);
            var direction = GetDirectionToTarget();
            _creature.UpdateSpriteDirection(direction);
        }

        protected IEnumerator GoToHero()
        {
            while (_vision.isTouchingLayer) 
            {
                if (_canAttack.isTouchingLayer)
                {
                    StartState(Attack());
                }
                else
                {
                    var horizontalDelta = Mathf.Abs(_target.transform.position.x 
                                                    - transform.position.x);
                    if (horizontalDelta <= _horizontalTrashold)
                        _creature.SetDirection(Vector2.zero);
                    else
                        SetDirectionToTarget();
                }
                
                yield return null;
            }
            
            _creature.SetDirection(Vector2.zero);
            _particles.Spawn("Miss");
            yield return new WaitForSeconds(_missHeroCooldown);

            DoubleCheckHero();
        }

        private void DoubleCheckHero()
        {
            if (_vision.isTouchingLayer)
                StartState(GoToHero());
            else
            {
                StartState(_patrol.DoPatrol());
            }
        }

        protected virtual IEnumerator Attack()
        {
            while (_canAttack.isTouchingLayer)
            {
                _creature.Attack();
                _particles.Spawn("Attack");
                yield return new WaitForSeconds(_attackCooldawn);
            }

            StartState(GoToHero());
        }

        protected virtual void SetDirectionToTarget()
        {
            var direction = GetDirectionToTarget();
            _creature.SetDirection(direction);
        }

        private Vector2 GetDirectionToTarget()
        {
            var direction = _target.transform.position - transform.position;
            direction.y = 0;
            return direction.normalized;
        }
        
        public virtual void OnHeroInVision(GameObject go)
        {
            if (_isDead) return;
            _target = go;
            
            StartState(AgroToHero());
        }

        public void OnDie()
        {
            _isDead = true;
            _animator.SetBool(IsDeadKey,true);

            if (_current != null)
            {
                StopCoroutine(_current);
            }
        }
    }
}