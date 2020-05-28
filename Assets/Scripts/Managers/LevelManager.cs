using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using TD.AI;
using UnityEngine;
using UnityEngine.UI;

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
        public bool infinite;
        public bool AutoStart;

        Wave currentWave;
        float lastWaveTime;
        float lastSpawnTime;
        int lastWave;
        bool isStarted = false;
        bool checkVictory = false;


        Queue<GameObject> spawnQueue;
        List<Mob> spawnedMobs;

        public Text GameOverText;
        public Button RestartButton;
        public GameObject GameOverMenu;

        private void Awake()
        {
            spawnedMobs = new List<Mob>();
            spawnQueue = new Queue<GameObject>();
            lastWaveTime = Time.time - timeBetweenWaves;
            lastSpawnTime = Time.time;
            lastWave = -1;
        }
        private void Start()
        {
            if(AutoStart)
            {
                StartLevel();
            }
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
                        if(waveList != null )
                        {
                            waveList.ClearWave(lastWave);

                        }
                        if(lastWave + 1 < waves.Count)
                        {
                            currentWave = waves[lastWave + 1];
                            if (currentWave.Mobs.Count > 0)
                            {
                                int newTimeBetweenWaves = 0;
                                for (int i = 0; i < currentWave.Mobs.Count; i++)
                                {
                                    for (int j = 0; j < currentWave.Mobs[i].Quantity; j++)
                                    {
                                        spawnQueue.Enqueue(currentWave.Mobs[i].Mob);
                                        newTimeBetweenWaves++;
                                    }
                                }
                                timeBetweenWaves = newTimeBetweenWaves;
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

                            if(infinite)
                            {
                                lastWave = -1;
                                isStarted = true;
                            }
                            else
                            {
                                checkVictory = true;
                            }
                        }
                        
                        
                    }

                    
                }
                else
                {
                    Debug.LogWarning("No waves assigned to this level, no spawning will occur");
                    isStarted = false;
                }
            }

            //If the time has passed the alloted time since the last spawn, spawn the next mob in the queue
            if (spawnQueue.Any() && Time.time > lastSpawnTime + timeBetweenSpawns)
            {
                spawnedMobs.Add(entry.Spawn(spawnQueue.Dequeue()));
                lastSpawnTime = Time.time;
                
            }

            if(checkVictory)
            {
                if(CheckMobs())
                {
                    ClockManager.instance.StopClock();
                    GameOverMenu.SetActive(true);
                    RestartButton.gameObject.SetActive(false);
                    GameOverText.text = "Victory! You haved saved the timeline!";
                }
            }
        }

        bool CheckMobs()
        {
            for (int i = 0; i < spawnedMobs.Count; i++)
            {
                if(spawnedMobs[i] != null)
                {
                    return false;
                }
            }

            return true;
        }
    }
}

