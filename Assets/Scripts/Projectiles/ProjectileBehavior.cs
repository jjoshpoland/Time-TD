using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TD.Projectiles
{
    public abstract class ProjectileBehavior : ScriptableObject
    {
        /// <summary>
        /// Called every frame by the projectile
        /// </summary>
        /// <param name="projectile"></param>
        public abstract void Move(Projectile projectile);

        /// <summary>
        /// Called once (probably by a turret) when the projectile is instantiated
        /// </summary>
        /// <param name="projectile"></param>
        public abstract void Initialize(Projectile projectile, Quaternion rotation);
    }
}

