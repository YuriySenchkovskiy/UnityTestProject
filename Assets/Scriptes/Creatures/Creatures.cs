using Scriptes.Components.Audio;
using Scriptes.Components.ColliderBased;
using Scriptes.Components.GoBased;
using UnityEngine;
using UnityEngine.Serialization;

namespace Scriptes.Creatures
{
    [RequireComponent(typeof(Animator), typeof(PlaySoundsComponent))]
    public class Creatures : MonoBehaviour
    {
        [Header("Movement")] 
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField, Range(0, 10)] private float _speed;

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
        [SerializeField] private int _damage; 
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
        
        public float Speed
        {
            get => _speed;
            set => _speed = value;
        }

        private float CalculateSpeed => _speed;
        
        public void UpdateSpriteDirection(Vector2 direction)
        {
            if (direction.x > 0)
            {
                transform.localScale = new Vector3(_forwardScale.x, _forwardScale.y, _forwardScale.z);
            }
            else if (direction.x < 0)
            {
                transform.localScale = new Vector3(_backwardsScale.x, _backwardsScale.y, _backwardsScale.z);
            }
        }
        
        public virtual void TakeDamage() 
        {
            _isJumping = false;
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _damageJumpLevel); 
        }
        
        public virtual void Attack()
        {
            _animator.SetTrigger(AttackKey);
            _sounds.Play(_melee);
        }
        
        public void DoAttack() 
        {
            _attackRange.Check(); 
        }

        public void SetDirection(Vector2 direction)
        {
            this._direction = direction; 
        }

        protected virtual void Awake()
        {
            _sounds = GetComponent<PlaySoundsComponent>();
        }

        protected virtual void Update()
        {
            _isGrounded = _layerCheck.IsTouchingLayer;
        }

        protected virtual void FixedUpdate()
        {
            var xVelocity = CalculateXVelocity(); 
            var yVelocity = CalculateYVelocity();
            _rigidbody2D.velocity = new Vector2(xVelocity, yVelocity); 
            
            UpdateAnimation(); 
            UpdateSpriteDirection(_direction); 
        }

        protected virtual float CalculateXVelocity()
        {
            return _direction.x * CalculateSpeed;
        }

        protected virtual float CalculateYVelocity() 
        {
            var yVelocity = _rigidbody2D.velocity.y; 
            var isJumpPressed = _direction.y > 0; 
            
            if (_isGrounded)
            {
                _isJumping = false; 
            }

            if (isJumpPressed) 
            {
                _isJumping = true; 
                
                var isFalling = _rigidbody2D.velocity.y <= 0.001f;
                var _isOnSliderPlatform = _rigidbody2D.velocity.y == _platformSpeed;;
                yVelocity = isFalling||_isOnSliderPlatform ? CalculateJumpVelocity(yVelocity) : yVelocity; 
            }
            else if (_rigidbody2D.velocity.y > 0 && _isJumping) 
            {
                yVelocity *= 0.85f; 
            }

            return yVelocity;
        }

        protected virtual float CalculateJumpVelocity(float yVelocity) 
        {
            if (_isGrounded) 
            {
                yVelocity = _jumpLevel; 
                DoJumpVfx();
            }
            
            return yVelocity;
        }

        protected void DoJumpVfx()
        {
            _particles.Spawn(_jump);
            _sounds.Play(_jump);
        }
        
        protected void SpawnFootDust()
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