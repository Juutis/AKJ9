using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    [SerializeField]
    private List<EnemySpawner> spawnPoints;
    [SerializeField]
    private List<WaveConfig> waves;
    [SerializeField]
    private EnemyConfig enemyConfig;

    [SerializeField]
    private int currentWaveIndex = 0;
    private WaveConfig currentWave;
    private WaveConfig previousWave;

    private SpawnPhase phase = SpawnPhase.Spawning;
    private bool spawningOnGoing = false;

    private float waitStarted = 0;

    void Start()
    {
        currentWave = waves[currentWaveIndex];
        for (int i = 0; i < 4; i++)
        {
            spawnPoints[i].SetEnemyConfig(enemyConfig);
        }
    }

    void Update()
    {
        if (currentWaveIndex == waves.Count - 1 && phase != SpawnPhase.Spawning)
        {
            bool finished = true;
            foreach (var go in GameObject.FindGameObjectsWithTag("Goblin"))
            {
                Goblin goblin = go.GetComponent<Goblin>();
                if (goblin.IsAlive())
                {
                    finished = false;
                    break;
                }
            }
            if (finished)
            {
                UIManager.main.ShowTheEnd();
            }
        }

        if (currentWaveIndex == waves.Count - 1)
        {
            UIManager.main.HideIntermissionInfo();
            UIManager.main.HideIntermissionTimer();
        }

        if (phase == SpawnPhase.Spawning)
        {
            //Debug.Log("Starting to spawn " + Time.fixedTime);
            if (!spawningOnGoing)
            {
                for (int i = 0; i < 4; i++)
                {
                    WaveSpawn wave = currentWave.GetSpawn(i);
                    //Batch batch = currentBatch[i];
                    if (wave.batches.Count > 0)
                    {
                        spawnPoints[i].SetWaveSpawn(wave);
                    }
                }

                spawningOnGoing = true;
            }

            // next wave when all the spawners have spawned their enemies
            if (spawnPoints.Aggregate(true, (prod, next) => prod && next.GetHasEnded()))
            {
                spawningOnGoing = false;
                phase = SpawnPhase.Waiting;
                waitStarted = Time.fixedTime;
            }
        }
        else if (phase == SpawnPhase.Waiting && (Time.fixedTime - waitStarted > currentWave.WaveEndWaitTime))
        {
            phase = SpawnPhase.Ended;
            UIManager.main.HideIntermissionInfo();
        }
        else if (phase == SpawnPhase.Waiting)
        {
            if(Input.GetKeyDown(KeyCode.Space) && TutorialManager.main.GetFinished())
            {
                skipWaveWaitingPeriod();
                UIManager.main.HideIntermissionInfo();
            } else {

                UIManager.main.UpdateIntermissionInfo("Wave " + (currentWaveIndex + 2) + " spawns in:");
                UIManager.main.UpdateIntermissionTimer(currentWave.WaveEndWaitTime - (Time.fixedTime - waitStarted));
            }
        }
        else if (phase == SpawnPhase.Ended)
        {
            waveEndStuff();
        }
    }

    private void skipWaveWaitingPeriod()
    {
        waveEndStuff();
        if (waves.Count > currentWaveIndex + 1) // no cheating at the end of the game!
        {
            var waited = Time.time - waitStarted;
            var waitedPerc = 1 - waited / previousWave.WaveEndWaitTime;
            Debug.Log("Skipped a wave waiting period! Multiplier duration = " + previousWave.MultiplierDuration * waitedPerc);
            ScoreManager.main.AddMultiplier(previousWave.MultiplierDuration * waitedPerc);
        }
    }

    private void waveEndStuff()
    {
        if (waves.Count > currentWaveIndex + 1)
        {
            previousWave = currentWave;
            currentWaveIndex++;
            currentWave = waves[currentWaveIndex];
            phase = SpawnPhase.Spawning;
        }
        else
        {
            //end
        }
    }
}

enum SpawnPhase
{
    Spawning,
    Waiting,
    Ended
}