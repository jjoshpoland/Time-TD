using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

namespace TD.AI
{
    [RequireComponent(typeof(Health), typeof(NavMeshAgent))]
    public class Mob : MonoBehaviour
    {
        NavMeshAgent agent;
        Health health;
        public float TimeValue;
        public float speed;
        public FloatEvent OnDie;
        public FloatEvent OnDelete;

        public float Scale
        {
            get
            {
                return agent.speed / speed;
            }
            set
            {
                agent.speed = speed * value;
            }
        }
        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            health = GetComponent<Health>();
            if(agent != null)
            {
                agent.SetDestination(GameObject.FindGameObjectWithTag("Exit").transform.position);
            }
            else
            {
                Debug.LogWarning("Mob has no NavMeshAgent attached");
            }
        }

        private void Start()
        {
            agent.speed = speed;
            health.OnDie.AddListener(Die);
        }

       

        public void RevertSpeed()
        {
            agent.speed = speed;
        }

        /// <summary>
        /// Sent to the health object, which handles dying
        /// </summary>
        public void Die()
        {
            OnDie.Invoke(TimeValue);
            Animator anim = GetComponent<Animator>();
            if(anim != null)
            {
                anim.SetTrigger("Death");
            }
            
        }

        public void Dead()
        {
            Destroy(gameObject);
        }
        
        public void Dead()
        {
            OnDie.Invoke(TimeValue);
            Destroy(gameObject);
        }
        /// <summary>
        /// Called by this object when it has been identified for deletion (i.e. when it exits without dying)
        /// </summary>
        public void Delete()
        {
            OnDelete.Invoke(TimeValue);
            health.OnDelete.Invoke();
            Destroy(gameObject);
        }
        
    }
}

[System.Serializable]
public class FloatEvent : UnityEvent<float>
{

}

