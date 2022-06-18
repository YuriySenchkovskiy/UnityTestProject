using Unity.Mathematics;
using UnityEngine;

namespace Weapon
{
    public class MachineGun : Weapon
    {
        [SerializeField] private Transform _secondBullet;
        [SerializeField] private Transform _thirdBullet;
        
        public override void Shoot()
        {
            AnimatorWeapon.SetTrigger(Fire);
            
            Instantiate(Bullet, ShootPoint.position, quaternion.identity);
            Instantiate(Bullet, _secondBullet.position, _secondBullet.rotation);
            Instantiate(Bullet, _thirdBullet.position, _thirdBullet.rotation);
            Instantiate(Effect, EffectPoint.position, quaternion.identity);
        }
    }
}