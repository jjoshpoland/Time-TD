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

