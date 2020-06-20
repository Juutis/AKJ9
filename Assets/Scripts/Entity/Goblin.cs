using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : MonoBehaviour
{
    private int health = 5;
    public void TakeDamage(int damage) {
        health -= damage;
        if (health <= 0) {
            health = 0;
            Die();
        }
    }

    private void Die() {
        Destroy(gameObject);
    }
}
