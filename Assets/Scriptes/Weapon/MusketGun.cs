using Unity.Mathematics;
using UnityEngine;

namespace Weapon
{
    public class MusketGun : Weapon
    {
        public override void Shoot(Transform shootPoint)
        {
            Shooted?.Invoke();
            Instantiate(Bullet, shootPoint.position, quaternion.identity);
            Instantiate(Effect, EfectPoint.position, Quaternion.identity);
        }
    }
}