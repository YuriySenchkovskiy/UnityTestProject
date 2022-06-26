using UnityEngine;
using UnityEngine.InputSystem;

namespace Creatures.Hero
{
    [RequireComponent(typeof(CreatureMover), typeof(CreatureAttack))]
    public class HeroInputReader : MonoBehaviour
    {
        private CreatureMover _creatureMover;
        private CreatureAttack _creatureAttack;
        private Vector2 _direction;

        private void Awake()
        {
            _creatureMover = GetComponent<CreatureMover>();
            _creatureAttack = GetComponent<CreatureAttack>();
        }

        public void DoHorizontalMovement(InputAction.CallbackContext context)
        {
            _direction.x = context.ReadValue<float>();
            _creatureMover.SetDirection(_direction); 
        }
        
        public void DoJump(InputAction.CallbackContext context)
        {
            _direction.y = context.ReadValue<float>();
            _creatureMover.SetDirection(_direction);
        }
        
        public void DoAttack(InputAction.CallbackContext context)
        {
            if (context.canceled)
            {
                _creatureAttack.Attack();
            }
        }
    }
}