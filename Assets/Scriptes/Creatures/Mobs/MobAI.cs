using System.Collections;
using Scriptes.Components.ColliderBased;
using Scriptes.Components.GoBased;
using Scriptes.Creatures.Mobs.Patrolling;
using UnityEngine;

namespace Scriptes.Creatures.Mobs
{
    [RequireComponent(typeof(Patrol), typeof(SpawnListComponent))]
    public class MobAI : MonoBehaviour
    {
        [SerializeField] private LayerCheck _isCanAttack;
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
        private bool _isDead; 
        
        private Patrol _patrol;
        private Coroutine _currentCoroutine;
        private WaitForSeconds _attackWait;
        
        private WaitForSeconds _cooldownWait;
        private WaitForSeconds _alarmWait;

        private void StartState(IEnumerator coroutine)
        {
            _creature.SetDirection(Vector2.zero); 
            
            if (_currentCoroutine != null)
            {
                StopCoroutine(_currentCoroutine);
            }
            
            _currentCoroutine = StartCoroutine(coroutine);
        }

        private IEnumerator AgroToHero()
        {
            LookAtHero();
            _particles.Spawn(_exclamation);
            yield return _alarmWait;
            StartState(GoToHero());
        }

        private void LookAtHero()
        {
            _creature.SetDirection(Vector2.zero);
            var direction = GetDirectionToTarget();
            _creature.UpdateSpriteDirection(direction);
        }

        private IEnumerator GoToHero()
        {
            while (_vision.IsTouchingLayer) 
            {
                if (_isCanAttack.IsTouchingLayer)
                {
                    StartState(Attack());
                }
                else
                {
                    var horizontalDelta = Mathf.Abs(_target.transform.position.x 
                                                    - transform.position.x);
                    if (horizontalDelta <= _horizontalTrashold)
                    {
                        _creature.SetDirection(Vector2.zero);
                    }
                    else
                    {
                        SetDirectionToTarget();
                    }
                }
                
                yield return null;
            }
            
            _creature.SetDirection(Vector2.zero);
            _particles.Spawn(_miss);
            yield return _cooldownWait;

            CheckHeroPosition();
        }
        
        private IEnumerator Attack()
        {
            while (_isCanAttack.IsTouchingLayer)
            {
                _creature.Attack();
                _particles.Spawn(_attack);
                yield return _attackWait;
            }

            StartState(GoToHero());
        }

        private void SetDirectionToTarget()
        {
            var direction = GetDirectionToTarget();
            _creature.SetDirection(direction);
        }

        private void CheckHeroPosition()
        {
            StartState(_vision.IsTouchingLayer ? GoToHero() : _patrol.DoPatrol());
        }
        
        private void Awake()
        {
            _particles = GetComponent<SpawnListComponent>();
            _patrol = GetComponent<Patrol>();
        }

        private void Start()
        {
            StartState(_patrol.DoPatrol());
            _attackWait = new WaitForSeconds(_attackCooldawn);
            _cooldownWait = new WaitForSeconds(_missHeroCooldown);
            _alarmWait = new WaitForSeconds(_alarmDelay); 
        }
        
        private Vector2 GetDirectionToTarget()
        {
            var direction = _target.transform.position - transform.position;
            direction.y = 0;
            return direction.normalized;
        }
    }
}