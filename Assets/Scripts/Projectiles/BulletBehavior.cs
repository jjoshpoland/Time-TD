using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.AI;

namespace TD.Projectiles
{
    [CreateAssetMenu(fileName = "Bullet", menuName = "TD/Projectiles/Bullet", order = 0)]
    public class BulletBehavior : ProjectileBehavior
    {
        public float speed;
        public DamageType type;
        public int damage;

        
        public override void Hit(Projectile projectile, GameObject target)
        {
            Health targetHealth = target.GetComponent<Health>();
            if(targetHealth != null)
            {
                //if the target wasnt immune, call the projectile on hit event, else call the on blocked event
                if(targetHealth.TakeDamage(damage, type))
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
        }

        public override void Initialize(Projectile projectile, Quaternion rotation)
        {
            projectile.transform.rotation = rotation;
        }

        public override void Move(Projectile projectile)
        {
            projectile.transform.position += (projectile.transform.forward * speed) * Time.deltaTime;
        }


    }
}

