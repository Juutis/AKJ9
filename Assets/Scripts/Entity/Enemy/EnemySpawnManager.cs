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

    void FixedUpdate()
    {
        if (phase == SpawnPhase.Spawning)
        {
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
        }
        else if (phase == SpawnPhase.Ended)
        {
            if (waves.Count > currentWaveIndex + 1)
            {
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
}

enum SpawnPhase
{
    Spawning,
    Waiting,
    Ended
}