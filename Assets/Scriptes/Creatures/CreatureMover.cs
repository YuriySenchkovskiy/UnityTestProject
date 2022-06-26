using Components.Audio;
using Components.ColliderBased;
using Components.GoBased;
using Creatures.Health;
using UnityEngine;

namespace Creatures
{
    [RequireComponent(typeof(Rigidbody2D), typeof(SpawnListComponent), typeof(Animator))]
    public class CreatureMover : MonoBehaviour
    {
        [Header("Movement")]
        [SerializeField, Range(0, 10)] private float _speed;
        [SerializeField] private bool _invertScale;

        private Rigidbody2D _rigidbody2D;
        private Vector2 _direction;
        private Vector3 _forwardScale = Vector3.one; 
        private Vector3 _backwardsScale = new Vector3(-1, 1, 1);
        
        [Space] [Header("Jumping")] 
        [SerializeField, Range(0, 100)] private float _jumpLevel; 
        [SerializeField, Range(0, 100)] private float _damageJumpLevel; 
        [SerializeField] private LayerCheck _layerCheck;
        [SerializeField] private string _jump = "Jump";
        
        private bool _isGrounded; 
        private bool _isJumping;
        
        [Space] [Header("Particles")]
        [SerializeField] private string _run = "Run";
       
        private static readonly int isRunning = Animator.StringToHash("isRunning");
        private static readonly int isGround = Animator.StringToHash("isGround");
        private static readonly int VerticalVelocity = Animator.StringToHash("verticalVelocity");
        
        private SpawnListComponent _particles;
        private float _minSpeed = 0.01f;
        private Animator _animator;
        private int _zeroValue = 0;
        
        private HealthComponent _healthComponent;
        private PlaySoundsComponent _sounds;
        private string _healSound = "Heal";
        private string _damageSound = "Damage";

        public float Speed
        {
            get => _speed;
            set => _speed = value;
        }

        private float CalculateSpeed => _speed;
        public bool IsGrounded => _isGrounded;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _particles = GetComponent<SpawnListComponent>();
            _animator = GetComponent<Animator>();
            _healthComponent = GetComponent<HealthComponent>();
            _sounds = GetComponent<PlaySoundsComponent>();
        }
        
        private void OnEnable()
        {
            _healthComponent.Damaged += TakeDamage;
            _healthComponent.Healed += TakeHeal;
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
            _healthComponent.Damaged -= TakeDamage;
            _healthComponent.Healed -= TakeHeal;
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
        
        public void SetDirection(Vector2 direction)
        {
            this._direction = direction; 
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
        
        private void SpawnFootDustInAnimation()
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
    }
}