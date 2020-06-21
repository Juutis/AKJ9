using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTower : MonoBehaviour
{
    private int hp = 5;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (hp <= 0)
        {
            Debug.Log("I die");
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int dmg)
    {
        hp -= dmg;
    }
}
