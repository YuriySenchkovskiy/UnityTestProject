using Scriptes.Components.Audio;
using Scriptes.Components.ColliderBased;
using Scriptes.Components.GoBased;
using UnityEngine;

namespace Scriptes.Creatures
{
    public class Creatures : MonoBehaviour
    {
        [Header("Movement")] 
        [SerializeField] protected Rigidbody2D _rigidbody2D;
        [SerializeField, Range(0, 10)] private float _speed;
        [SerializeField] private bool _invertScale;
        
        protected Vector2 direction;
        protected Vector3 forwardScale = Vector3.one; 
        protected Vector3 backwardsScale = new Vector3(-1, 1, 1);

        [Space] [Header("Jumping")] 
        [SerializeField, Range(0, 100)] protected float jumpLevel; 
        [SerializeField, Range(0, 100)] protected float damageJumpLevel; 
        [SerializeField] private LayerCheck _layerCheck;
        [SerializeField] private string _jump = "Jump";
        
        protected bool _isGrounded; 
        protected bool _isJumping; 
        private float _platformSpeed;
        
        [Space] [Header("Attack")]
        [SerializeField] private int _damage; 
        [SerializeField] private CheckCircleOverlap _attackRange;
        [SerializeField] private string _melee = "Melee";

        [Space] [Header("Particles")] 
        [SerializeField] protected SpawnListComponent _particles;
        [SerializeField] private string _run = "Run";
        
        private float _minSpeed = 0.01f;
        
        [Space] [Header("Animator")]
        [SerializeField] protected Animator _animator; 
        
        public static readonly int Hit = Animator.StringToHash("hit");
        public static readonly int AttackKey = Animator.StringToHash("attack");
        private static readonly int isRunning = Animator.StringToHash("isRunning");
        private static readonly int isGround = Animator.StringToHash("isGround");
        
        private static readonly int verticalVelocity = Animator.StringToHash("verticalVelocity");
        private int _zeroValue = 0;
        
        protected PlaySoundsComponent _sounds;
        
        public float Speed
        {
            get => _speed;
            set => _speed = value;
        }
        
        public void UpdateSpriteDirection(Vector2 direction)
        {
            var multiplier = _invertScale ? -1 : 1;
            
            if (direction.x > 0)
            {
                transform.localScale = new Vector3(forwardScale.x * multiplier, forwardScale.y, forwardScale.z);
            }
            else if (direction.x < 0)
            {
                transform.localScale = new Vector3(backwardsScale.x * multiplier, backwardsScale.y, backwardsScale.z);
            }
        }
        
        public virtual void TakeDamage() 
        {
            _isJumping = false;
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, damageJumpLevel); 
        }
        
        public virtual void Attack()
        {
            _animator.SetTrigger(AttackKey);
            _sounds.Play(_melee);
        }
        
        public void OnDoAttack() 
        {
            _attackRange.Check(); 
        }

        public void SetDirection(Vector2 direction)
        {
            this.direction = direction; 
        }

        protected virtual void Awake()
        {
            _sounds = GetComponent<PlaySoundsComponent>();
        }

        protected virtual void Update()
        {
            _isGrounded = _layerCheck.isTouchingLayer;
        }

        protected virtual void FixedUpdate()
        {
            var xVelocity = CalculateXVelocity(); 
            var yVelocity = CalculateYVelocity();
            _rigidbody2D.velocity = new Vector2(xVelocity, yVelocity); 
            
            UpdateAnimation(); 
            UpdateSpriteDirection(direction); 
        }

        protected virtual float CalculateXVelocity()
        {
            return direction.x * CalculateSpeed();
        }

        protected virtual float CalculateSpeed()
        {
            return _speed;
        }

        protected virtual float CalculateYVelocity() 
        {
            var yVelocity = _rigidbody2D.velocity.y; 
            var isJumpPressed = direction.y > 0; 
            
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
                yVelocity = jumpLevel; 
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
            _animator.SetFloat(verticalVelocity, _rigidbody2D.velocity.y); 
            _animator.SetBool(isRunning, direction.x != _zeroValue && _speed != _zeroValue); 
        }
    }
}