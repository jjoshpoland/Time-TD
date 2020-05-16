using System.Collections;
using System.Collections.Generic;
using TD.AI;
using UnityEngine;
using UnityEngine.Events;

public class Exit : MonoBehaviour
{
    public UnityEvent OnMobEnter;

    private void OnTriggerEnter(Collider other)
    {
        Mob mob = other.gameObject.GetComponent<Mob>();
        if (mob != null)
        {
            Health mobHealth = mob.gameObject.GetComponent<Health>();
            if(mobHealth != null && mobHealth.isDead)
            {
                return;
            }
            OnMobEnter.Invoke();
            mob.Delete();
        }
    }

}
