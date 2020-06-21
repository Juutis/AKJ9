using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Goblin : MonoBehaviour
{
    private NavMeshAgent agent;
    private int health = 5; //TODO: Config!
    private float agentSpeed; //TODO: Config!
    private float freezed = 0f;
    private float freezeStarted = 0f;
    private float freezeMult = 1f;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agentSpeed = agent.speed;
    }

    private void FixedUpdate()
    {
        if (freezeStarted != 0 && freezeMult < 1)
        {
            if(Time.fixedTime - freezeStarted > freezed)
            {
                freezeMult = 1;
                freezeStarted = 0;
            }

            agent.speed = agentSpeed * freezeMult;
        }
    }

    public void TakeDamage(EnergyTypeConfig energyConfig)
    {
        if (energyConfig.FreezeTime > 0)
        {
            freezeStarted = Time.fixedTime;
            freezed = energyConfig.FreezeTime;
            freezeMult = energyConfig.FreezeMultiplier;
        }

        health -= energyConfig.Damage;
        if (health <= 0)
        {
            health = 0;
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
