using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TD.Managers
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField]
        Spawner entry;
        //add exit point here
        [SerializeField]
        List<Wave> waves;
        [SerializeField]
        float timeBetweenWaves;
        [SerializeField]
        float timeBetweenSpawns;

        Wave currentWave;
        float lastWaveTime;
        float lastSpawnTime;
        int lastWave;
        bool isStarted = false;

        Queue<GameObject> spawnQueue;

        private void Awake()
        {
            spawnQueue = new Queue<GameObject>();
            lastWaveTime = Time.time;
            lastSpawnTime = Time.time;
            lastWave = -1;
        }

        public void StartLevel()
        {
            isStarted = true;
        }

        private void Update()
        {
            if(isStarted)
            {
                //Ensure there are waves assigned to the level. Throw a warning if not.
                if (waves.Count > 0)
                {
                    //If the time has passed the alloted time since the last wave, then queue up the spawns for the next wave
                    if(Time.time > lastWaveTime + timeBetweenWaves)
                    {
                        currentWave = waves[lastWave + 1];
                        if(currentWave.Mobs.Count > 0)
                        {
                            for (int i = 0; i < currentWave.Mobs.Count; i++)
                            {
                                spawnQueue.Enqueue(currentWave.Mobs[i]);
                            }
                            lastWaveTime = Time.time;

                        }
                        else
                        {
                            Debug.LogWarning("No mobs are assigned to the current wave. No spawning will occur");
                        }
                        
                    }

                    //If the time has passed the alloted time since the last spawn, spawn the next mob in the queue
                    if(Time.time > lastSpawnTime + timeBetweenSpawns)
                    {
                        entry.Spawn(spawnQueue.Dequeue());
                        lastSpawnTime = Time.time;
                    }
                }
                else
                {
                    Debug.LogWarning("No waves assigned to this level, no spawning will occur");
                    isStarted = false;
                }
            }
        }
    }
}

