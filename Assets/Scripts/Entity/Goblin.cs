using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Goblin : MonoBehaviour
{
    private HitPointBar hpBar;

    private NavMeshAgent agent;
    [SerializeField]
    private int score = 5;
    [SerializeField]
    private float maxHealth = 5;

    private float health = 5;
    private float agentSpeed;
    private float freezed = 0f;
    private float freezeStarted = 0f;
    private float freezeMult = 1f;
    private Pool pool;
    private bool isAlive = false;

    public void Initialize()
    {
        health = 5;
        freezed = 0f;
        freezeStarted = 0f;
        freezeMult = 1f;
        isAlive = true;
        if (hpBar != null)
        {
            hpBar.Show();
        }
    }

    private void Start()
    {
        hpBar = WorldSpaceUI.main.GetHitPointBar(health);
        agent = GetComponent<NavMeshAgent>();
        agentSpeed = agent.speed;
        pool = GetComponent<Pooled>().GetPool();
        Initialize();
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
            KillHPBar();
            Die();
        }
        hpBar.UpdateHp(health);
    }

    void Update() {
        hpBar.UpdatePosition(transform.position);
    }

    public void KillHPBar() {
        hpBar.Hide();
    }
    private void Die()
    {
        ScoreManager.main.AddScore(score);
        //Destroy(gameObject);
        pool.DeactivateObject(gameObject);
        isAlive = false;

    }

    public bool IsAlive()
    {
        return isAlive;
    }
}
