using System.Collections;
using System.Collections.Generic;
using UnityEditor.iOS;
using UnityEngine;

namespace TD.Projectiles
{
    [CreateAssetMenu(fileName = "Missile", menuName = "TD/Projectiles/Missile", order = 2)]
    public class MissileBehavior : ProjectileBehavior
    {
        public float speed;
        public DamageType type;
        public int damage;
        public float explosionRadius;
        public GameObject ExplosionEffect;

        public override void Hit(Projectile projectile, GameObject target)
        {
            if (ExplosionEffect != null)
            {
                Instantiate(ExplosionEffect).transform.position = projectile.transform.position;
            }

            Collider[] hits = Physics.OverlapSphere(projectile.transform.position, explosionRadius, LayerMask.GetMask("Enemy"));

            for (int i = 0; i < hits.Length; i++)
            {
                Health targetHealth = hits[i].gameObject.GetComponent<Health>();
                if (targetHealth != null)
                {
                    targetHealth.TakeDamage(damage, type);
                }
            }

            
        }

        public override void Initialize(Projectile projectile, Quaternion rotation)
        {
            
        }

        public override void Move(Projectile projectile, GameObject target)
        {
            if(target != null)
            {
                //Find the desired rotation by getting the difference between this position and the target position
                Quaternion targetRotation = Quaternion.LookRotation(target.transform.position - projectile.transform.position);

                //Determine the amount of rotation by interpolating between the current rotation and the target rotation by a factor of time
                Quaternion rotation = Quaternion.Slerp(projectile.transform.localRotation, targetRotation, 10f * Time.deltaTime); //turn the 20 into a parameter



                //Set the local rotation to the interpolated rotation
                projectile.transform.localRotation = rotation;
            }


            //projectile.transform.position += (projectile.transform.forward * (speed * scale)) * Time.deltaTime;
            projectile.transform.position += (projectile.transform.forward * (speed)) * Time.deltaTime;
        }
    }

}
