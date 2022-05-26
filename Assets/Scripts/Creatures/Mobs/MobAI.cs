using System.Collections;
using Scriptes.Components.ColliderBased;
using Scriptes.Components.GoBased;
using Scriptes.Creatures.Mobs.Patrolling;
using UnityEngine;

namespace Scriptes.Creatures.Mobs
{
    public class MobAI : MonoBehaviour
    {
        [SerializeField] private LayerCheck _canAttack;
        [SerializeField] private Creatures _creature;
        [SerializeField] private float _horizontalTrashold = 0.2f;
        [SerializeField] private float _alarmDelay = 0.5f; 
        
        [SerializeField] private float _attackCooldawn = 1f;
        [SerializeField] private float _missHeroCooldown = 0.5f;
        [SerializeField] private LayerCheck _vision;
        [SerializeField] private string _attack = "Attack";
        
        [SerializeField] private string _miss = "Miss";
        [SerializeField] private string _exclamation = "Exclamation";
        
        private GameObject _target; 
        private SpawnListComponent _particles;
        private Animator _animator;
        private bool _isDead; 
        
        private Patrol _patrol;
        private IEnumerator _current;
        private static readonly int IsDeadKey = Animator.StringToHash("isDeadKey");
        
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
            _particles.Spawn(_exclamation); 
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
            _particles.Spawn(_miss);
            yield return new WaitForSeconds(_missHeroCooldown);

            DoubleCheckHero();
        }
        
        protected virtual IEnumerator Attack()
        {
            while (_canAttack.isTouchingLayer)
            {
                _creature.Attack();
                _particles.Spawn(_attack);
                yield return new WaitForSeconds(_attackCooldawn);
            }

            StartState(GoToHero());
        }

        protected virtual void SetDirectionToTarget()
        {
            var direction = GetDirectionToTarget();
            _creature.SetDirection(direction);
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
        
        private Vector2 GetDirectionToTarget()
        {
            var direction = _target.transform.position - transform.position;
            direction.y = 0;
            return direction.normalized;
        }
    }
}