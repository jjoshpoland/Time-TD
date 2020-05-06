using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace TD.AI
{
    public class Mob : MonoBehaviour
    {
        NavMeshAgent agent;

        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            if(agent != null)
            {
                agent.SetDestination(GameObject.FindGameObjectWithTag("Exit").transform.position);
            }
            else
            {
                Debug.LogWarning("Mob has no NavMeshAgent attached");
            }
        }
        
    }
}

