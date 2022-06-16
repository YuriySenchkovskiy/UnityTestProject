using Unity.Mathematics;
using UnityEngine;

namespace Weapon
{
    public class Musket : Weapon
    {
        public override void Shoot(Transform shootPoint)
        {
            Instantiate(Bullet, shootPoint.position, quaternion.identity);
        }
    }
}