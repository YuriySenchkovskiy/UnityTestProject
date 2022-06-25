using UnityEngine;
using UnityEngine.InputSystem;

namespace Creatures.Hero
{
    public class HeroInputReader : MonoBehaviour
    {
        [SerializeField] private Creatures _hero;
        private Vector2 _direction;
        
        public void DoHorizontalMovement(InputAction.CallbackContext context)
        {
            _direction.x = context.ReadValue<float>();
            _hero.SetDirection(_direction); 
        }
        
        public void DoJump(InputAction.CallbackContext context)
        {
            _direction.y = context.ReadValue<float>();
            _hero.SetDirection(_direction);
        }
    }
}