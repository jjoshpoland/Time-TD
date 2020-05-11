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
        Debug.Log("Mob escaped");
        Mob mob = other.gameObject.GetComponent<Mob>();
        if (mob != null)
        {
            OnMobEnter.Invoke();
            mob.Delete();
        }
    }

}
