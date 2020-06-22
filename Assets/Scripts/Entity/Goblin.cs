﻿using System.Collections;
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
    private float health = 5;
    private float agentSpeed;
    private float freezed = 0f;
    private float freezeStarted = 0f;
    private float freezeMult = 1f;

    private void Start()
    {
        hpBar = WorldSpaceUI.main.GetHitPointBar(health);
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
            KillHPBar();
            Die();
        }
        hpBar.UpdateHp(health);
    }

    void Update() {
        hpBar.UpdatePosition(transform.position);
    }

    public void KillHPBar() {
        hpBar.Die();
    }
    private void Die()
    {
        ScoreManager.main.AddScore(score);
        Destroy(gameObject);
    }
}
