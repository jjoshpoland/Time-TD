using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TD.Towers
{
    /// <summary>
    /// 
    /// </summary>
    [RequireComponent(typeof(Turret), typeof(LineRenderer))]
    public class LaserGenerator : MonoBehaviour
    {
        Turret turret;
        LineRenderer laserLine;
        // Start is called before the first frame update
        void Start()
        {
            turret = GetComponent<Turret>();
            laserLine = GetComponent<LineRenderer>();

            if(turret == null)
            {
                Debug.LogError("Laser generator requires a turret to work. Destroying.");
                Destroy(this);
            }

            if(laserLine == null)
            {
                Debug.LogError("Laser generator requires a line renderer to work. Destroying.");
                Destroy(this);
            }

            laserLine.enabled = false;
        }

        private void Update()
        {
            if (turret.Target != null && laserLine.enabled)
            {
                laserLine.SetPosition(0, turret.Muzzle.transform.position);
                laserLine.SetPosition(1, turret.Target.transform.position);
            }
        }

        public void StartLaser()
        {
            if(turret.Target != null)
            {
                laserLine.enabled = true;
                laserLine.SetPosition(0, turret.Muzzle.transform.position);
                laserLine.SetPosition(1, turret.Target.transform.position);
            }
        }

        public void EndLaser()
        {
            laserLine.enabled = false;
        }
    }
}

