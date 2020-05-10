using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace TD.AI
{
    [RequireComponent(typeof(Health), typeof(NavMeshAgent))]
    public class Mob : MonoBehaviour
    {
        NavMeshAgent agent;
        Health health;

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

        public void Delete()
        {
            health.OnDelete.Invoke();
            Destroy(gameObject);
        }
        
    }
}

