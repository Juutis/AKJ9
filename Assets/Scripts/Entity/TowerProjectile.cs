using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerProjectile : MonoBehaviour
{
    private Goblin target;
    private float duration = 0.5f;
    private float minDistance = 0.01f;
    private int damage = 1;
    private float timer = 0f;
    private Vector3 startPos;
    public void Launch(Goblin targetGoblin)
    {
        target = targetGoblin;
        startPos = transform.position;
    }

    void Update()
    {
        if (target != null)
        {
            timer += Time.deltaTime;
            Vector3 targetPos = target.transform.position;
            targetPos.y = 0.5f;
            transform.position = Vector3.Lerp(startPos, targetPos, timer / duration);
            if (Vector3.Distance(transform.position, targetPos) < minDistance) {
                target.TakeDamage(damage);
                Die();
            }
        }
    }

    private void Die() {
        Destroy(gameObject);
    }
}
