using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTower : MonoBehaviour
{
    private int hp = 5;
    private HitPointBar hitPointBar;
    void Start()
    {
        hitPointBar = UIManager.main.GetHitPointBar(hp);
        Vector3 hpBarPos = transform.position;
        hpBarPos.y = 4f;
        hitPointBar.SetSize(new Vector2(200, 20));
        hitPointBar.UpdatePosition(hpBarPos);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (hp <= 0)
        {
            Debug.Log("I die");
            hitPointBar.Die();
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int dmg)
    {
        hp -= dmg;
        hitPointBar.UpdateHp(hp);
    }
}
