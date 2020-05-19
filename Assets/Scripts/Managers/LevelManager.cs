using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        public WaveList waveList;

        Wave currentWave;
        float lastWaveTime;
        float lastSpawnTime;
        int lastWave;
        bool isStarted = false;

        Queue<GameObject> spawnQueue;

        private void Awake()
        {
            spawnQueue = new Queue<GameObject>();
            lastWaveTime = Time.time - timeBetweenWaves;
            lastSpawnTime = Time.time;
            lastWave = -1;
        }

        public void StartLevel()
        {
            isStarted = true;
        }

        public Wave[] GetWaves()
        {
            if(waves.Count <= 0)
            {
                return null;
            }

            Wave[] waveList = new Wave[waves.Count];

            for (int i = 0; i < waves.Count; i++)
            {
                waveList[i] = waves[i];
            }

            return waveList;
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
                        waveList.ClearWave(lastWave);
                        if(lastWave + 1 < waves.Count)
                        {
                            currentWave = waves[lastWave + 1];
                            if (currentWave.Mobs.Count > 0)
                            {
                                for (int i = 0; i < currentWave.Mobs.Count; i++)
                                {
                                    for (int j = 0; j < currentWave.Mobs[i].Quantity; j++)
                                    {
                                        spawnQueue.Enqueue(currentWave.Mobs[i].Mob);
                                    }
                                }
                                lastWaveTime = Time.time;
                                lastWave++;
                            }
                            else
                            {
                                Debug.LogWarning("No mobs are assigned to the current wave. No spawning will occur");
                            }
                        }
                        else
                        {
                            isStarted = false;
                            Debug.Log("All waves have spawned");
                        }
                        
                        
                    }

                    //If the time has passed the alloted time since the last spawn, spawn the next mob in the queue
                    if(spawnQueue.Any() && Time.time > lastSpawnTime + timeBetweenSpawns)
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

