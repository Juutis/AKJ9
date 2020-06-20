using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyConfig", menuName = "New EnemyConfig")]
public class EnemyConfig : ScriptableObject
{
    [SerializeField]
    private List<EnemyConfigItem> enemies;
    public List<EnemyConfigItem> Enemies { get { return enemies; } set { enemies = value; } }
}

[Serializable]
public class EnemyConfigItem
{
    public EnemyType type;
    public GameObject prefab;
}