using System.Collections;
using System.Collections.Generic;
using TD.Managers;
using UnityEngine;
using UnityEngine.Events;
using TD.AI;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    GameObject spawnPoint;

    public UnityEvent OnSpawn;
    
    /// <summary>
    /// Spawn the mob at the spawn point
    /// </summary>
    /// <param name="mob"></param>
    public Mob Spawn(GameObject mob)
    {
        Mob newMob = Instantiate(mob, spawnPoint.transform).GetComponent<Mob>();
        newMob.transform.parent = null;
        newMob.OnDie.AddListener(ClockManager.instance.AddTime); //clock manager should add time when this mob dies
        newMob.OnDelete.AddListener(ClockManager.instance.RemoveTime); //clock manager should remove time if this mob escapes
        OnSpawn.Invoke();
        return newMob;
    }
}
