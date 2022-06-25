using UnityEngine;

namespace Creatures.Health
{
    public class ChangeHealthComponent : MonoBehaviour
    {
        [SerializeField] private int _changeValue;
        
        public void ApplyDamage(GameObject target)
        {
            var healthComponent = target.GetComponent<HealthComponent>();
            if (healthComponent != null)
            {
                healthComponent.ApplyDamage(_changeValue);
            }
        }
        
        public void ApplyHeal(GameObject target)
        {
            var healthComponent = target.GetComponent<HealthComponent>();
            if (healthComponent != null)
            {
                healthComponent.ApplyHeal(_changeValue);
            }
        }
    }
}