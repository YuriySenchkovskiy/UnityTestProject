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
            Instantiate(Effect, shootPoint.position, quaternion.identity);
            
            //Instantiate(Bullet, ShootPoint.position, quaternion.identity);
            //Instantiate(Effect, ShootPoint.position, quaternion.identity);
        }
    }
}