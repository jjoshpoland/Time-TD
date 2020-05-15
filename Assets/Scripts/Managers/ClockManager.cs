using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TD.AI;
using TD.Projectiles;
using TD.Towers;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace TD.Managers
{
    public class ClockManager : MonoBehaviour
    {
        public static ClockManager instance;

        public float StartingTime;
        float remainingTime;
        [Range(0f, 1f)]
        public float TimeScale;
        public Text Clock;
        bool isStopped;
        public UnityEvent OnZero;
        public float ConstraintDuration;
        float constraintTime;

        private void Awake()
        {
            instance = this;
        }
        // Start is called before the first frame update
        void Start()
        {
            isStopped = true;
            remainingTime = StartingTime;
        }

        // Update is called once per frame
        void Update()
        {
            if(Time.time > constraintTime + ConstraintDuration)
            {
                TimeScale = 1f;
                CalculateConstraints();
            }

            if(!isStopped)
            {
                remainingTime -= (Time.deltaTime * TimeScale);
                if (remainingTime <= 0f)
                {
                    OnZero.Invoke();
                    isStopped = true;
                    enabled = false;
                }
            }
            
            Clock.text = TimeSpan.FromSeconds(remainingTime).ToString("mm':'ss':'ff");
            Clock.color = Color.Lerp(Clock.color, Color.white, .01f);
        }

        public void StartClock()
        {
            isStopped = false;
        }

        /// <summary>
        /// Adds the given time from the clock. 
        /// </summary>
        /// <param name="time">Will be calculated as absolute value</param>
        public void AddTime(float time)
        {
            remainingTime += Mathf.Abs(time);
            Clock.color = Color.green;
        }

        /// <summary>
        /// Removes the given time from the clock
        /// </summary>
        /// <param name="time">Will be calculated as absolute value</param>
        public void RemoveTime(float time)
        {
            remainingTime -= Mathf.Abs(time);
            Clock.color = Color.red;
        }

        public void SetScale(float scale)
        {
            TimeScale = scale;
        }

        public void CalculateConstraints()
        {
            Mob[] mobs = FindObjectsOfType<Mob>();
            Projectile[] projectiles = FindObjectsOfType<Projectile>();
            Turret[] turrets = FindObjectsOfType<Turret>();

            if(turrets.Length >= 0)
            {
                for (int i = 0; i < turrets.Length; i++)
                {
                    turrets[i].scale = TimeScale;
                }
            }

            if(mobs.Length >= 0)
            {
                for (int i = 0; i < mobs.Length; i++)
                {
                    mobs[i].Scale = TimeScale;
                }
            }

            if(projectiles.Length >= 0)
            {
                for (int i = 0; i < projectiles.Length; i++)
                {
                    projectiles[i].Scale = TimeScale;
                }
            }
            constraintTime = Time.time;

            //TimeScale = .5f;
            //TimeConstraint[] constraints = GameObject.FindObjectsOfType<TimeConstraint>();
            //if(constraints.Length <= 0)
            //{
            //    Debug.LogWarning("no time constrainable objects found in scene");
            //    return;
            //}

            //for (int i = 0; i < constraints.Length; i++)
            //{
            //    if (constraints[i] != null)
            //    {
            //        constraints[i].Impose(ConstraintDuration);
            //    }
            //    else
            //    {
            //        Debug.LogWarning("attempting to impose a time constraint on a null object");
            //    }
            //}
        }
    }
}

