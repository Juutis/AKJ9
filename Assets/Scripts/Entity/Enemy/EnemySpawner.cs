using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;
    [SerializeField]
    private Transform spawnPoint;

    private WaveSpawn wave;
    private Batch currentBatch;
    private int currentBatchIndex = 0;
    private int enemiesSpawned = 0;
    private Pool pool;
    private EnemyConfig enemyConfig;

    private bool started = false;
    private bool wait = true;
    private float waitingStarted = 0;
    private float lastSpawned = 0;

    // Start is called before the first frame update
    void Start()
    {
    }

    private void FixedUpdate()
    {
        if (!started) return;

        if (wait)
        {
            //Debug.Log("Waiting to start spawning");
            if (Time.fixedTime - waitingStarted > currentBatch.waitTime)
            {
                wait = false;
            }
        }
        else if (enemiesSpawned < currentBatch.enemyCount)
        {
            if (Time.fixedTime - lastSpawned > currentBatch.spawnTime)
            {
                pool.ActivateObject(spawnPoint.position);
                lastSpawned = Time.fixedTime;
                enemiesSpawned++;
            }
        }
        else
        {
            currentBatchIndex++;
            if (wave.batches.Count > currentBatchIndex)
            {
                currentBatch = wave.batches[currentBatchIndex];
                GameObject prefab = enemyConfig.Enemies.Where(x => x.type == currentBatch.enemyType).First().prefab;
                wait = true;
                waitingStarted = Time.fixedTime;
                enemiesSpawned = 0;
            }
            else
            {
                started = false;
            }
        }
    }

    public void SetWaveSpawn(WaveSpawn wave)
    {
        this.wave = wave;
        currentBatchIndex = 0;
        currentBatch = wave.batches[currentBatchIndex];
        GameObject prefab = enemyConfig.Enemies.Where(x => x.type == currentBatch.enemyType).First().prefab;
        pool = ObjectPooler.GetPool(prefab);
        wait = true;
        started = true;
        waitingStarted = Time.time;
    }

    public void SetEnemyConfig(EnemyConfig config)
    {
        enemyConfig = config;
    }

    public bool GetHasEnded()
    {
        return !started;
    }
}