using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TD.Projectiles
{
    [CreateAssetMenu(fileName = "Rocket", menuName = "TD/Projectiles/Rocket", order = 3)]
    public class RocketBehavior : ProjectileBehavior
    {
        public float speed;
        public int damage;
        public float explosionRadius;
        public GameObject ExplosionEffect;
        public override void Hit(Projectile projectile, GameObject target)
        {
            Collider[] hits = Physics.OverlapSphere(projectile.transform.position, explosionRadius, LayerMask.GetMask("Enemy"));

            for (int i = 0; i < hits.Length; i++)
            {
                Health targetHealth = hits[i].gameObject.GetComponent<Health>();
                if (targetHealth != null)
                {
                    targetHealth.TakeDamage(damage, type);
                }
            }

            if (ExplosionEffect != null)
            {
                Instantiate(ExplosionEffect).transform.position = projectile.transform.position;
            }
        }

        public override void Initialize(Projectile projectile, Quaternion rotation)
        {
            projectile.transform.rotation = rotation;
        }

        public override void Move(Projectile projectile, GameObject target)
        {
            //projectile.transform.position += (projectile.transform.forward * (speed * scale)) * Time.deltaTime;
            projectile.transform.position += (projectile.transform.forward * (speed)) * Time.deltaTime;
        }
    }
}

