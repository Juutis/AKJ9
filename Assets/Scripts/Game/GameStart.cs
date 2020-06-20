using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    void Start()
    {
        BaseTower baseTower = Prefabs.Instantiate<BaseTower>();
        baseTower.transform.position = Vector3.zero;
        
        Energy energy = Prefabs.Instantiate<Energy>();
        energy.transform.position = new Vector3(0f, 0f, 5f);

        Tower tower = Prefabs.Instantiate<Tower>();
        tower.transform.position = new Vector3(5f, 0f, 5f);
    }

    void Update()
    {
        
    }
}
