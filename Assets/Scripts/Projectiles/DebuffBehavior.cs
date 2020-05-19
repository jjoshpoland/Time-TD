using System.Collections;
using System.Collections.Generic;
using TD.Debuffs;
using TD.Projectiles;
using UnityEngine;

namespace TD.Projectiles
{
    [CreateAssetMenu(fileName = "AOEDebuffBehavior", menuName = "TD/Projectiles/AOEDebuff", order = 6)]
    public class DebuffBehavior : ProjectileBehavior
    {
        public float ProjectileSpeed;
        public float Radius;
        public Debuff debuff;
        public GameObject AOEEffect;
        public override void Hit(Projectile projectile, GameObject target)
        {


        }

        public override void Initialize(Projectile projectile, Quaternion rotation)
        {
        }

        public override void Move(Projectile projectile, GameObject target)
        {
            Instantiate(AOEEffect, projectile.transform).transform.parent = null;
            Collider[] hits = Physics.OverlapSphere(projectile.transform.position, Radius, LayerMask.GetMask("Enemy"));

            for (int i = 0; i < hits.Length; i++)
            {
                DebuffManager debuffManager = hits[i].gameObject.GetComponent<DebuffManager>();
                if (debuffManager != null)
                {
                    debuffManager.AddDebuff(debuff);
                }
            }

            Destroy(projectile.gameObject);
        }
    }
}

