using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TD.Projectiles
{
    [CreateAssetMenu(fileName = "Laser", menuName = "TD/Projectiles/Laser", order = 1)]
    public class LaserBehavior : ProjectileBehavior
    {
        public int damage;
        public DamageType type;

        public override void Hit(Projectile projectile, GameObject target)
        {
            
        }

        public override void Initialize(Projectile projectile, Quaternion rotation)
        {
            
        }

        public override void Move(Projectile projectile, float scale, GameObject target)
        {
            projectile.transform.position = target.transform.position; //may need to change this to be a raycast to the target and see where it hits their collider or mesh
            Health targetHealth = target.GetComponent<Health>();
            if (targetHealth != null)
            {
                //if the target wasnt immune, call the projectile on hit event, else call the on blocked event
                if (targetHealth.TakeDamage(damage, type))
                {
                    projectile.OnHit.Invoke();
                }
                else
                {
                    projectile.OnBlocked.Invoke();
                }
            }
            else
            {
                projectile.OnBlocked.Invoke();
            }

            Destroy(projectile.gameObject);
        }

    }
}

