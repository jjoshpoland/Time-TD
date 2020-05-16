using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TD.Projectiles;
using UnityEngine.AI;
using UnityEngine.Events;

namespace TD.Towers
{
    public class Turret : MonoBehaviour
    {
        public float Cost;
        [SerializeField]
        GameObject target;
        [SerializeField]
        GameObject turretBase;
        [SerializeField]
        GameObject muzzle;
        [SerializeField]
        Projectile ProjectilePrefab;
        [SerializeField]
        float Range;
        [SerializeField]
        float fireRate;
        [SerializeField]
        [Range(-1, 1)]
        float predictionRate;
        public float scale;
        [SerializeField]
        Sprite icon;
        [SerializeField]
        public List<Turret> upgradePaths;
        


        public bool Initialized;
        float lastShoot;

        public UnityEvent OnShoot;
        public UnityEvent OnTargetAcquired;
        public UnityEvent OnTargetLost;

        public GameObject Target
        {
            get
            {
                return target;
            }
            private set
            {
                target = value;
            }
        }

        public GameObject Muzzle
        {
            get
            {
                return muzzle;
            }
            private set
            {
                muzzle = value;
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            scale = 1f;
            lastShoot = Time.time;
        }

        // Update is called once per frame
        void Update()
        {
            if(Initialized)
            {
                EvaluateTargets();
                LookAtTarget();
                Shoot();
            }
            
        }

        void Shoot()
        {
            if(Time.time > lastShoot + fireRate && target != null)
            {
                Projectile newProjectile = Instantiate(ProjectilePrefab, null);
                newProjectile.transform.position = muzzle.transform.position;
                newProjectile.Target = target;
                newProjectile.behavior.Initialize(newProjectile, turretBase.transform.localRotation);
                newProjectile.Scale = scale;
                lastShoot = Time.time;
                OnShoot.Invoke();
            }
            
        }

        /// <summary>
        /// Returns the turret this turret will upgrade to if the given index is selected
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Turret GetUpgradePath(int index)
        {
            if(index >= 0 && index < upgradePaths.Count)
            {
                return upgradePaths[index];
            }
            else
            {
                Debug.LogError(index + " is invalid for the UpgradePaths list for " + name);
                return null;
            }
        }

        /// <summary>
        /// Rotates the turret base toward the target
        /// </summary>
        void LookAtTarget()
        {
            if(target != null)
            {
                //Find the desired rotation by getting the difference between this position and the target position
                Quaternion targetRotation = Quaternion.LookRotation((target.transform.position + (target.GetComponent<NavMeshAgent>().velocity * predictionRate)) - transform.position);

                //Determine the amount of rotation by interpolating between the current rotation and the target rotation by a factor of time
                Quaternion rotation = Quaternion.Slerp(turretBase.transform.localRotation, targetRotation, 10f * Time.deltaTime); //turn the 20 into a parameter

                //Set the local rotation to the interpolated rotation
                turretBase.transform.localRotation = rotation;
                //Set the x axis and z axis rotations to zero since the turret should only be rotating on the y axis
                turretBase.transform.localEulerAngles = new Vector3(0, turretBase.transform.localEulerAngles.y, 0);
            }
        }

        void EvaluateTargets()
        {
            //first check if there is an existing target and if it is in range. clear the target if not
            if (target != null && Vector3.Distance(transform.position, target.transform.position) > Range)
            {
                target = null;
                OnTargetLost.Invoke();
            }
            if(target != null && target.GetComponent<Health>().isDead)
            {
                target = null;

                OnTargetLost.Invoke();
            }
            //then check if there is still a target
            if (target != null)
            {
                Debug.Log(target.GetComponent<Health>().isDead);
                return;
            }
            


            Collider[] hits = Physics.OverlapSphere(transform.position, Range, LayerMask.GetMask("Enemy"));

            GameObject closestEnemy = null;
            float closestDist = float.MaxValue;

            for (int i = 0; i < hits.Length; i++)
            {
                float distance = Vector3.Distance(transform.position, hits[i].transform.position);
                Health targetHealth = hits[i].gameObject.GetComponent<Health>();
                if (targetHealth != null && targetHealth.isDead)
                {
                    continue;
                }
                if (closestEnemy == null) //could reverse this to optimize if needed
                {
                    closestEnemy = hits[i].gameObject;
                    closestDist = distance;
                }
                else
                {
                    if(distance < closestDist)
                    {
                        closestEnemy = hits[i].gameObject;
                        closestDist = distance;
                    }
                }
            }

            if(closestEnemy != null)
            {
                target = closestEnemy;
                OnTargetAcquired.Invoke();
            }
            else
            {
                OnTargetLost.Invoke();
            }

        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, Range);

            if(target != null)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(muzzle.transform.position, target.transform.position);
            }
        }
    }
}

