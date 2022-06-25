using Components.Audio;
using Components.ColliderBased;
using Components.GoBased;
using Components.Health;
using UnityEngine;
using UnityEngine.Serialization;

namespace Creatures
{
    [RequireComponent(typeof(Animator), typeof(PlaySoundsComponent), typeof(Health))]
    public class Creatures : MonoBehaviour
    {
        [Header("Movement")] 
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField, Range(0, 10)] private float _speed;
        [SerializeField] private bool _invertScale;

        private Vector2 _direction;
        private Vector3 _forwardScale = Vector3.one; 
        private Vector3 _backwardsScale = new Vector3(-1, 1, 1);

        [FormerlySerializedAs("jumpLevel")]
        [Space] [Header("Jumping")] 
        [SerializeField, Range(0, 100)] private float _jumpLevel; 
        [SerializeField, Range(0, 100)] private float _damageJumpLevel; 
        [SerializeField] private LayerCheck _layerCheck;
        [SerializeField] private string _jump = "Jump";
        
        private bool _isGrounded; 
        private bool _isJumping; 
        private float _platformSpeed;
        
        [Space] [Header("Attack")]
        [SerializeField] private CheckCircleOverlap _attackRange;
        [SerializeField] private string _melee = "Melee";

        [Space] [Header("Particles")] 
        [SerializeField] private SpawnListComponent _particles;
        [SerializeField] private string _run = "Run";
        
        private float _minSpeed = 0.01f;
        
        [Space] [Header("Animator")]
        [SerializeField] private Animator _animator; 
        
        private static readonly int Hit = Animator.StringToHash("hit");
        private static readonly int AttackKey = Animator.StringToHash("attack");
        private static readonly int isRunning = Animator.StringToHash("isRunning");
        private static readonly int isGround = Animator.StringToHash("isGround");
        
        private static readonly int VerticalVelocity = Animator.StringToHash("verticalVelocity");
        private int _zeroValue = 0;
        
        private PlaySoundsComponent _sounds;
        private string _healSound = "Heal";
        private string _damageSound = "Damage";

        private Health _health;
        
        public float Speed
        {
            get => _speed;
            set => _speed = value;
        }

        private float CalculateSpeed => _speed;
        
        private void Awake()
        {
            _health = GetComponent<Health>();
            _sounds = GetComponent<PlaySoundsComponent>();
        }

        private void OnEnable()
        {
            _health.Damaged += TakeDamage;
            _health.Healed += TakeHeal;
        }
        
        private void Update()
        {
            _isGrounded = _layerCheck.IsTouchingLayer;
        }
        
        private void FixedUpdate()
        {
            var xVelocity = CalculateXVelocity(); 
            var yVelocity = CalculateYVelocity();
            _rigidbody2D.velocity = new Vector2(xVelocity, yVelocity); 
            
            UpdateAnimation(); 
            UpdateSpriteDirection(_direction); 
        }
        
        private void OnDisable()
        {
            _health.Damaged -= TakeDamage;
            _health.Healed -= TakeHeal;
        }
        
        public void UpdateSpriteDirection(Vector2 direction)
        {
            var multiplier = _invertScale ? -1 : 1;
            if (direction.x > 0)
            {
                transform.localScale = new Vector3(_forwardScale.x * multiplier, _forwardScale.y, _forwardScale.z);
            }
            else if (direction.x < 0)
            {
                transform.localScale = new Vector3(_backwardsScale.x * multiplier, _backwardsScale.y, _backwardsScale.z);
            }
        }

        public void Attack()
        {
            _animator.SetTrigger(AttackKey);
            _particles.Spawn(_melee);
            _sounds.Play(_melee);
        }
        
        public void DoAttack() // используется в аниматоре!
        {
            _attackRange.Check(); 
        }

        public void SetDirection(Vector2 direction)
        {
            this._direction = direction; 
        }

        private void TakeDamage() 
        {
            _isJumping = false;
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _damageJumpLevel);
            _sounds.Play(_damageSound);
        }

        private void TakeHeal()
        {
            _sounds.Play(_healSound);
        }

        private float CalculateXVelocity()
        {
            return _direction.x * CalculateSpeed;
        }

        private float CalculateYVelocity() 
        {
            var yVelocity = _rigidbody2D.velocity.y; 
            var isJumpPressed = _direction.y > 0;
            var lowJumpLevel = 0.85f;
            
            if (_isGrounded)
            {
                _isJumping = false; 
            }

            if (isJumpPressed) 
            {
                _isJumping = true; 
                
                var isFalling = _rigidbody2D.velocity.y <= 0.001f;
                yVelocity = isFalling ? CalculateJumpVelocity(yVelocity) : yVelocity; 
            }
            else if (_rigidbody2D.velocity.y > 0 && _isJumping) 
            {
                yVelocity *= lowJumpLevel; 
            }

            return yVelocity;
        }

        private float CalculateJumpVelocity(float yVelocity) 
        {
            if (_isGrounded) 
            {
                yVelocity = _jumpLevel; 
                DoJumpVfx();
            }
            
            return yVelocity;
        }

        private void DoJumpVfx()
        {
            _particles.Spawn(_jump);
            _sounds.Play(_jump);
        }

        private void SpawnFootDust() // вызывается в аниматоре
        {
            if (_isGrounded && _rigidbody2D.velocity.y <= _minSpeed)
            {
                _particles.Spawn(_run); 
            }
        }
        
        private void UpdateAnimation() 
        {
            _animator.SetBool(isGround, _isGrounded);
            _animator.SetFloat(VerticalVelocity, _rigidbody2D.velocity.y); 
            _animator.SetBool(isRunning, _direction.x != _zeroValue && _speed != _zeroValue); 
        }
    }
}