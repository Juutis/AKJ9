﻿using System;
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

    [SerializeField]
    private GameObject dieEffect;

    [SerializeField]
    private GameObject deathSoundPrefab;

    private float health = 5;
    private float agentSpeed;
    private float freezed = 0f;
    private float freezeStarted = 0f;
    private float freezeMult = 1f;
    private Pool pool;
    private bool isAlive = false;

    public void Initialize()
    {
        health = maxHealth;
        freezed = 0f;
        freezeStarted = 0f;
        freezeMult = 1f;
        isAlive = true;
        if (hpBar != null)
        {
            hpBar.UpdateHp(health);
            hpBar.Show();
        }
    }

    private void Start()
    {
        hpBar = UIManager.main.GetHitPointBar(health);
        agent = GetComponent<NavMeshAgent>();
        agentSpeed = agent.speed;
        pool = GetComponent<Pooled>().GetPool();
        Initialize();
    }

    private void FixedUpdate()
    {
        if (freezeStarted != 0 && freezeMult < 1)
        {
            if (Time.fixedTime - freezeStarted > freezed)
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

    void Update()
    {
        hpBar.UpdatePosition(transform.position + Vector3.up * 0.3f);
    }

    public void KillHPBar()
    {
        hpBar.Hide();
    }
    private void Die()
    {
        var effectPos = transform.position + Vector3.down * 1.0f;
        var effect = ObjectPooler.GetPool(dieEffect).ActivateObject(effectPos).GetComponent<OneShotEffect>();
        effect.Play();
        GameObject x = Instantiate(deathSoundPrefab);
        x.transform.position = transform.position;

        int addedScore = ScoreManager.main.AddScore(score);
        BillboardScore billboardScore = Prefabs.Instantiate<BillboardScore>();
        billboardScore.Initialize(transform.position, addedScore);
        //Destroy(gameObject);
        pool.DeactivateObject(gameObject);
        isAlive = false;
    }

    public void SetAlive(bool alive)
    {
        isAlive = alive;
    }

    public bool IsAlive()
    {
        return isAlive;
    }
}
