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
        Tower tower2 = Prefabs.Instantiate<Tower>();
        tower2.transform.position = new Vector3(7f, 0f, 7f);

        GameObject goblin = Prefabs.Get<Goblin>();
        for (int i = 0; i < 10; i += 1) {
            GameObject newGoblin = Instantiate(goblin, tower.transform.position, Quaternion.identity);
            Vector2 rnd = Random.insideUnitCircle.normalized * 5f;
            newGoblin.transform.Translate(new Vector3(rnd.x, 0f, rnd.y), Space.World);
        }
    }
}
