using Unity.Mathematics;

namespace Weapon
{
    public class MusketGun : Weapon
    {
        public override void Shoot()
        {
            AnimatorWeapon.SetTrigger(Fire);
            Instantiate(Bullet, ShootPoint.position, quaternion.identity);
            Instantiate(Effect, EffectPoint.position, quaternion.identity);
        }
    }
}