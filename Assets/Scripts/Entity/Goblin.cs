using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : MonoBehaviour
{
    private int health = 5;
    private HitPointBar hpBar;

    void Start() {
        hpBar = WorldSpaceUI.main.GetHitPointBar(health);
    }

    public void TakeDamage(int damage) {
        health -= damage;
        hpBar.UpdateHp(health);
        if (health <= 0) {
            health = 0;
            KillHPBar();
            Destroy(gameObject);
        }
    }

    void Update() {
        hpBar.UpdatePosition(transform.position);
    }

    public void KillHPBar() {
        hpBar.Die();
    }
}
