using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    GameObject spawnPoint;

    public UnityEvent OnSpawn;
    
    public void Spawn(GameObject mob)
    {
        GameObject newMob = Instantiate(mob, spawnPoint.transform);
        newMob.transform.parent = null;
        OnSpawn.Invoke();
    }
}
