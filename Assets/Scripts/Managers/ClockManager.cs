using System;
using System.Collections;
using System.Collections.Generic;
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

        private void Awake()
        {
            instance = this;
        }
        // Start is called before the first frame update
        void Start()
        {
            remainingTime = StartingTime;
        }

        // Update is called once per frame
        void Update()
        {
            if(!isStopped)
            {
                remainingTime -= Time.deltaTime;
                if (remainingTime <= 0f)
                {
                    OnZero.Invoke();
                    isStopped = true;
                }
            }
            
            Clock.text = TimeSpan.FromSeconds(remainingTime).ToString("mm':'ss':'ff");
            Clock.color = Color.Lerp(Clock.color, Color.white, .01f);
        }

        /// <summary>
        /// Adds the given time from the clock. 
        /// </summary>
        /// <param name="time">Will be calculated as absolute value</param>
        public void AddTime(float time)
        {
            if(isStopped)
            {
                return;
            }
            remainingTime += Mathf.Abs(time);
            Clock.color = Color.green;
        }

        /// <summary>
        /// Removes the given time from the clock
        /// </summary>
        /// <param name="time">Will be calculated as absolute value</param>
        public void RemoveTime(float time)
        {
            if (isStopped)
            {
                return;
            }
            remainingTime -= Mathf.Abs(time);
            Clock.color = Color.red;
        }
    }
}

