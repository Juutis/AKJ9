using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "WaveConfig", menuName = "New WaveConfig")]
public class WaveConfig : ScriptableObject
{
    [SerializeField]
    private float waveEndWaitTime;
    public float WaveEndWaitTime { get { return waveEndWaitTime; } }

    [SerializeField]
    private WaveSpawn spawn1;
    public WaveSpawn Spawn1 { get { return spawn1; } }

    [SerializeField]
    private WaveSpawn spawn2;
    public WaveSpawn Spawn2 { get { return spawn2; } }

    [SerializeField]
    private WaveSpawn spawn3;
    public WaveSpawn Spawn3 { get { return spawn3; } }

    [SerializeField]
    private WaveSpawn spawn4;
    public WaveSpawn Spawn4 { get { return spawn4; } }

    public WaveSpawn GetSpawn(int i)
    {
        switch (i)
        {
            case 0: return spawn1;
            case 1: return spawn2;
            case 2: return spawn3;
            case 3: return spawn4;
            default: return spawn1;
        }
    }
}

[Serializable]
public class WaveSpawn
{
    public List<Batch> batches;
}

[Serializable]
public class Batch
{
    [Tooltip("moi")]
    public float waitTime;
    public float spawnTime;
    public int enemyCount;
    public EnemyType enemyType;
}