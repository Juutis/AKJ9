using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNavigation : MonoBehaviour
{
    private NavMeshAgent agent;
    private BaseTower castle;

    private Goblin goblin;
    private Pool pool;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        goblin = GetComponent<Goblin>();
        pool = GetComponent<Pooled>().GetPool();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(castle == null)
        {
            //Destroy(gameObject);
            return;
        }

        agent.SetDestination(castle.transform.position);

        if(Vector3.Distance(transform.position, castle.transform.position) < 1.2f)
        {
            goblin.KillHPBar();
            Debug.Log("I hit you!");
            castle.TakeDamage(1);

            pool.DeactivateObject(gameObject);
            goblin.SetAlive(false);
        }
    }

    public void SetTarget(BaseTower target)
    {
        this.castle = target;
    }
}
