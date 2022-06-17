using Unity.Mathematics;
using UnityEngine;

namespace Weapon
{
    public class MachineGun : Weapon
    {
        public override void Shoot(Transform shootPoint)
        {
            Shooted?.Invoke();

            Instantiate(Bullet, ShootPoint.position, quaternion.identity);
            Instantiate(Effect, EffectPoint.position, quaternion.identity);
        }
    }
}